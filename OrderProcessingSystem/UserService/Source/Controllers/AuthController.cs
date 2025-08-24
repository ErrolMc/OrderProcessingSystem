using Microsoft.AspNetCore.Mvc;
using OPS.UserService.Data;
using OPS.UserService.Repositories;
using OPS.UserService.Services;

namespace OPS.UserService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(IUserRepository userRepository, IAuthService authService, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _authService = authService;
            _logger = logger;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            if (await _userRepository.GetUserByUsernameAsync(request.Username) is not null)
                return BadRequest("Username already exists");

            string userID = Guid.NewGuid().ToString();
            var user = new User()
            {
                ID = userID,
                Username = request.Username,
                PasswordHash = _authService.HashPassword(request.Password),
            };

            await _userRepository.CreateUserAsync(user);

            _logger.LogInformation($"User {user.Username} registered");
            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            User user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user is null || !_authService.ValidatePassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password");
            
            _logger.LogInformation($"User {user.Username} logged in");
            return Ok(user.ToUserDto());
        }
    }
}
