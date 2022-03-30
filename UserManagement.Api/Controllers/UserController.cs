
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Shared.Models;
using UserManagement.Services;

namespace UserManagement.Api.Controllers
{
     [ApiController]
    [Route("[controller]/users/")]
    public class UserController : ControllerBase
    {
        private IUserService userService;      

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
          var allUsers = await userService.GetAllUsers();
          return allUsers is null ? NotFound("No users found") : Ok(allUsers);         
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
          var user = await userService.GetSingleUser(id);
          return user is null ? NotFound(string.Format("User details not found for the user id {0}", id)) : Ok(user); 
        }

        [HttpPost]        
        public async Task<IActionResult> AddNewUser(User user)
        {
           var result = await userService.AddNewUser(user);
           return result ? Ok() : Problem("Failed to add user"); 
        }

        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
          var result = await userService.DeleteUser(userId);
          return result ? Ok() : NotFound(string.Format("Unable to delete,User details not found for the user id {0}", userId));
        }

        [HttpPatch]       
        public async Task<IActionResult> UpdateUser(User userData)
        {
          var result = await userService.UpdateUser(userData);
         return result ? Ok() : Problem("Failed to update user");
        }
    }
}