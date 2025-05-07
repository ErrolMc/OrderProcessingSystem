using Microsoft.AspNetCore.Mvc;
using OPS.UserService.Data;
using OPS.UserService.Repositories;

namespace OPS.UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserInfoController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("getuserfromid")]
        public async Task<IActionResult> GetUserFromID([FromBody] string userID)
        {
            User user = await _userRepository.GetUserByIdAsync(userID);
            
            if (user is null)
                return NotFound($"Cant find user with id {userID}");
            
            return Ok(user.ToUserDto());
        }
        
        [HttpGet("getuserfromname")]
        public async Task<IActionResult> GetUserFromUsername([FromBody] string username)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            
            if (user is null)
                return NotFound($"Cant find user with name {username}");
            
            return Ok(user.ToUserDto());
        }
        
        [HttpDelete("deleteuserfromid")]
        public async Task<IActionResult> DeleteUserFromID([FromBody] string userID)
        {
            User user = await _userRepository.GetUserByIdAsync(userID);
            if (user is null)
                return NotFound($"Cant find user with id {userID}");

            bool result = await _userRepository.DeleteUserAsync(userID);
            if (!result)
                return NotFound($"Error deleting user with id {userID}");
            
            return Ok($"Deleted user {userID}");
        }
    }
}
