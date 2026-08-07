using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureVaultAPI.Models;
using SecureVaultAPI.Services;

namespace SecureVaultAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordService _service;

        public PasswordController(PasswordService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var passwords = await _service.GetAllAsync(userId);
            return Ok(passwords);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var password = await _service.GetByIdAsync(id, userId);
            if (password == null) return NotFound();
            return Ok(password);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Password model)
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            model.UserId = userId;
            var password = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = password.Id }, password);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Password model)
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var password = await _service.UpdateAsync(id, userId, model);
            if (password == null) return NotFound();
            return Ok(password);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var deleted = await _service.DeleteAsync(id, userId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}