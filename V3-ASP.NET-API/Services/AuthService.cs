using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureVaultAPI.Data;
using SecureVaultAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SecureVaultAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User?> RegisterAsync(AuthRegisterRequest request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (existingUser != null) throw new Exception("Username already exists.");

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = hash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string?> LoginAsync(AuthLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null) throw new Exception("Invalid username or password.");
            
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) throw new Exception("Invalid username or password.");

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userName", user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
