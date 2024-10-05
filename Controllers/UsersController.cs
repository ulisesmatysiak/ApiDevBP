using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;
using static SQLite.SQLite3;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // crear nuevo user
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {           
            try
            {
                var result = await _userService.SaveUserAsync(user);
                if (!result)
                {
                    _logger.LogWarning($"error save user: {user.Name} {user.Lastname}");
                    return BadRequest($"error save user: {user.Name} {user.Lastname}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error save: {ex.Message}");
                return StatusCode(500, $"error save: {ex.Message}");
            }
        }

        // obtener user
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();             
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get: {ex.Message}");
                return StatusCode(500, $"error get: {ex.Message}");
            }
        }

        // actualizar user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel user)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, user);
                if (!result)
                {
                    _logger.LogWarning($"error update user id: {id}");
                    return BadRequest("error update user id: {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error update: {ex.Message}");
                return StatusCode(500, $"error update: {ex.Message}");
            }
        }

        // eliminar user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"error delete user id: {id}");
                    return BadRequest("error delete user id: {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error delete: {ex.Message}");
                return StatusCode(500, $"error delete: {ex.Message}");
            }
        }

    }
}
