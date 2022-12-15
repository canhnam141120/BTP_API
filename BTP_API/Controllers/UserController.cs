namespace BookTradingPlatform.Controllers
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
        public async Task<IActionResult> login([FromForm] LoginVM loginVM)
        {
            try
            {
                var apiResponse = await _userRepository.loginAsync(loginVM);
                 return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.LOGIN_FAILED.ToString() });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> logout([FromForm] string token)
        {
            try
            {
                var apiMessage = await _userRepository.logoutAsync(token);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.LOGOUT_FAILED.ToString() });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromForm] RegisterVM registerVM)
        {
            try
            {
                var apiMessage = await _userRepository.registerAsync(registerVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.REGISTER_FAILED.ToString() });
            }
        }

        [HttpPut("verify-email")]
        public async Task<IActionResult> Verify([FromForm] string verifyCode)
        {
            try
            {
                var apiMessage = await _userRepository.verifyAsync(verifyCode);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.VERIFY_FAILED.ToString() });
            }

        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            try
            {
                var apiMessage = await _userRepository.forgotPasswordAsync(email);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordVM resetPasswordVM)
        {
            try
            {
                var apiMessage = await _userRepository.resetPasswordAsync(resetPasswordVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CHANGE_PASSWORD_FAILED.ToString() });
            }
        }
    }
}


