using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Newtonsoft.Json;
using Project_CreateAPI.Models;
using Project_CreateAPI.Repositories;

namespace Project_CreateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        [EnableQuery]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null && existingUser.Password == user.Password)
            {
                return Ok(existingUser);
            }
            return Unauthorized("Invalid email or password!");
        }
    }
}
