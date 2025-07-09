using UserManagementAPI.Data;
using UserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(AppDbContext users) : ControllerBase
    {
        private readonly AppDbContext _users = users;

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _users.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public ActionResult<User> Get(int Id)
        { 
            var user = _users.Users.Find(Id);
            if (user == null)
            {
                return NotFound($"User with Id {Id} not found");
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
            _users.Users.Add(user);
            await _users.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Id = user.Id }, user);
        }

    }
}
