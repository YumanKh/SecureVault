using SecureVaultAPI.Data;
using SecureVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SecureVaultAPI.Services
{
    public class PasswordService
    {
        private readonly AppDbContext _context;

        public PasswordService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Password>> GetAllAsync(int userId)
        {
            return await _context.Passwords.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Password?> GetByIdAsync(int id, int userId)
        {
            return await _context.Passwords.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        }

        public async Task<Password> CreateAsync(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
            return password;
        }

        public async Task<Password?> UpdateAsync(int id, int userId, Password updated)
        {
            var password = await _context.Passwords
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (password == null) return null;

            password.Title = updated.Title;
            password.Value = updated.Value;

            await _context.SaveChangesAsync();
            return password;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var password = await _context.Passwords
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (password == null) return false;

            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
