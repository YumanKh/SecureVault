using Microsoft.AspNetCore.Mvc;
using SecureVaultAPI.Services;
using SecureVaultAPI.Models;

namespace SecureVaultAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service) { _service = service; }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest model)
        {
            try
            {
                var user = await _service.RegisterAsync(model);
                return Ok(new { Id = user.Id, UserName = user.UserName });
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while registering the user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest model)
        {
            try
            {
                var token = await _service.LoginAsync(model);
                return Ok(new { Token = token });
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while logging in.");
            }
        }

    }
}
