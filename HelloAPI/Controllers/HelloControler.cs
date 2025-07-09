using UserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet("{Name}")]
        public ActionResult<Message> Get(string Name)
        {
            var massage = new Message
            {
                MassageText = $"Hello, {Name}!"
            };
            return Ok(massage);
        }
    }
}
