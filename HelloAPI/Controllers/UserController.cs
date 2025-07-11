﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _users = userRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _users.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            await _users.AddAsync(user);
            await _users.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Id = user.Id }, user);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int id, User updateUser)
        {
            if (id != updateUser.Id)
            {
                return BadRequest("Invalid user data");
            }
            var userToUpdate = await _users.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound($"User with Id {id} not found");
            }
            userToUpdate.Name = updateUser.Name;
            userToUpdate.Email = updateUser.Email;
            userToUpdate.LastUpdateAt = DateTime.UtcNow;
            await _users.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }
            _users.Delete(user);
            await _users.SaveChangesAsync();
            return NoContent();

        }
    }
}
