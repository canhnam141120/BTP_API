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
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                else
                {
                    if (apiResponse.Message == Message.ACCOUNT_NOT_EXIST.ToString())
                    {
                        return NotFound(apiResponse);
                    }
                    return BadRequest(apiResponse);
                }
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.LOGIN_FAILED.ToString() });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            try
            {
                var apiMessage = await _userRepository.logoutAsync();
                if(apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
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
                if (apiMessage.Message.Contains(Message.REGISTER_SUCCESS.ToString()))
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
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
                if (apiMessage.Message == Message.VERIFY_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
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
                if (apiMessage.Message.Contains(Message.SUCCESS.ToString()))
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
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
                if (apiMessage.Message == Message.ACCOUNT_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.CHANGE_PASSWORD_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CHANGE_PASSWORD_FAILED.ToString() });
            }
        }

        [HttpPost("new-token")]
        public async Task<IActionResult> RenewToken([FromHeader] TokenModel tokenModel)
        {
            try
            {
                var apiResponse = await _userRepository.renewTokenAsync(tokenModel);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return BadRequest(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
            }

        }

    }
}


