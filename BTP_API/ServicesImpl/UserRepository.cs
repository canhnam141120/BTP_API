using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using BTP_API.ViewModels;

namespace BTP_API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly BTPContext _context;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(BTPContext context, IOptionsMonitor<AppSettings> optionsMonitor, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> loginAsync(LoginVM loginVM)
        {
            Jwt jwt = new Jwt(_context, _appSettings, _config);
            Cookie cookie = new Cookie(_httpContextAccessor);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginVM.Email);
            if (user == null)
            {
                return new ApiResponse
                {
                    Message = Message.ACCOUNT_NOT_EXIST.ToString()
                };
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password);
            if (!isValid)
            {
                return new ApiResponse
                {
                    Message = Message.PASSWORD_INCORRECT.ToString()
                };
            }

            if (user.IsVerify == false)
            {
                return new ApiResponse
                {
                    Message = Message.ACCOUNT_NOT_VERIFY.ToString()
                };
            }

            if (user.IsActive == false)
            {
                return new ApiResponse
                {
                    Message = Message.ACCOUNT_IS_BAN.ToString()
                };
            }

            //Cấp token
            var token = jwt.GenerateToken(user);
            cookie.SetaccessToken(token);

            return new ApiResponse
            {
                Message = Message.LOGIN_SUCCESS.ToString(),
                Data = token,
                NumberOfRecords = 1
            };
        }

        public async Task<ApiMessage> logoutAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            option.Secure = true;
            option.IsEssential = true;
            _httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", string.Empty, option);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", string.Empty, option);
            //Then delete the cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("accessToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
            return new ApiMessage { Message = Message.LOGOUT_SUCCESS.ToString() };
        }
        public async Task<ApiMessage> registerAsync(RegisterVM registerVM)
        {
            CreateRandomToken random = new CreateRandomToken();
            var user = await _context.Users.AnyAsync(u => u.Email == registerVM.Email);
            if (user == true)
            {
                return new ApiMessage
                {
                    Message = Message.EMAIL_IS_EXIST.ToString()
                };
            }

            int costParameter = 12;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, costParameter);
            //bool test = BCrypt.Net.BCrypt.Verify(registerVM.Password, hashedPassword);

            var newUser = new User
            {
                RoleId = 3,
                Email = registerVM.Email,
                Password = hashedPassword,
                VerifyCode = random.CreateRandomCode4Byte(),
                Fullname = registerVM.Fullname,
                Phone = registerVM.Phone,
                AddressMain = registerVM.AddressMain,
                IsActive = true,
                LikeNumber = 0,
                NumberOfTransaction = 0
            };
            _context.Add(newUser);
            await _context.SaveChangesAsync();

            var getNewUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == registerVM.Email);
            if (getNewUser != null)
            {
                var newShippingUser = new ShipInfo
                {
                    UserId = getNewUser.Id,
                    IsUpdate = false
                };
                _context.Add(newShippingUser);
                await _context.SaveChangesAsync();
            }

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            mail.To.Add(MailboxAddress.Parse(registerVM.Email.Trim()));
            mail.Subject = "[Trạm Sách] - Vui lòng xác thực tài khoản";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<h3>Xin chào " + registerVM.Fullname + "!</h3>" +
                "<p>Mã xác thực tài khoản của bạn là: " + newUser.VerifyCode + "</p>" +
                "<p>Hãy xác thực tài khoản của bạn ngay lập tức để có thể sử dụng web!</p>" +
                "<p>Nếu có vấn đề phát sinh xảy ra, hãy liên hệ chúng tôi qua hotline: 0961284654</p>" +
                "<p>Trân trọng!</p>" +
                "<p>Hỗ trợ từ Trạm Sách!</p>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);

            return new ApiMessage
            {
                Message = Message.REGISTER_SUCCESS.ToString() + " - PLEASE GET CODE VERIFY FROM YOUR MAIL BOX!"
            };
        }
        public async Task<ApiMessage> verifyAsync(string verifyCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.VerifyCode == verifyCode);

            if(user != null)
            {
                if (user.IsVerify)
                {
                    return new ApiMessage
                    {
                        Message = Message.ACCOUNT_IS_VERIFY.ToString()
                    };
                }
                user.IsVerify = true;
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.VERIFY_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.CODE_NOT_MATCH.ToString()
            };
        }
        public async Task<ApiMessage> forgotPasswordAsync(string email)
        {
            CreateRandomToken random = new CreateRandomToken();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.EMAIL_NOT_EXIST.ToString()
                };
            }
            user.ForgotPasswordCode = random.CreateRandomCode4Byte();
            await _context.SaveChangesAsync();

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            mail.To.Add(MailboxAddress.Parse(user.Email.Trim()));
            mail.Subject = "[Trạm Sách] - Cấp mã code đổi lại mật khẩu";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<h3>Xin chào " + user.Fullname + "!</h3>" +
                "<p>Mã code để bạn đặt lại mật khẩu là: " + user.ForgotPasswordCode + "</p>" +
                "<p>Hãy thực hiện thay đổi ngay lập tức trước khi mã code hết hạn rồi đăng nhập lại để kiểm tra!</p>" +
                "<p>Nếu có vấn đề phát sinh xảy ra, hãy liên hệ chúng tôi qua hotline: 0961284654</p>" +
                "<p>Trân trọng!</p>" +
                "<p>Hỗ trợ từ Trạm Sách!</p>"
            };


            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);

            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString() + " - PLEASE GET CODE RESET PASSWORD FROM YOUR MAIL!"
            };
        }
        public async Task<ApiMessage> resetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {

            var check = await _context.Users.AnyAsync(u => u.Email == resetPasswordVM.Email);
            if (!check)
            {
                return new ApiMessage
                {
                    Message = Message.ACCOUNT_NOT_EXIST.ToString()
                };
            }

            var user = await _context.Users.SingleOrDefaultAsync
                (u => u.Email == resetPasswordVM.Email && u.ForgotPasswordCode == resetPasswordVM.ForgotPasswordCode);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.CODE_INCORRECT.ToString()
                };
            }

            int costParameter = 12;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordVM.NewPassword, costParameter);

            user.Password = hashedPassword;
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.CHANGE_PASSWORD_SUCCESS.ToString()
            };
        }
        public async Task<ApiResponse> renewTokenAsync(TokenModel tokenModel)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            ConvertUnixTimeToDateTime converDate = new ConvertUnixTimeToDateTime();
            Jwt jwt = new Jwt(_context, _appSettings, _config);
            Cookie cookie = new Cookie(_httpContextAccessor);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false
            };//ko kiểm tra token hết hạn

            //check 1: AccessToken valid format
            var tokenInVerification = jwtTokenHandler.ValidateToken(tokenModel.AccessToken, tokenValidateParam, out var validatedToken);

            //check 2: Check alg
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                if (!result)//false
                {
                    return new ApiResponse
                    {
                        Message = Message.INVALID_TOKEN.ToString()
                    };
                }
            }

            //check 3: Check accessToken expire?
            var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expireDate = converDate.Convert(utcExpireDate);
            if (expireDate > DateTime.Now)
            {
                return new ApiResponse
                {
                    Message = Message.ACCESS_TOKEN_NOT_YET_EXPIRED.ToString()
                };
            }

            //check 4: Check refreshtoken exist in DB
            var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenModel.RefreshToken);
            if (storedToken == null)
            {
                return new ApiResponse
                {
                    Message = Message.REFRESH_TOKEN_NOT_EXIST.ToString()
                };
            }

            //check 5: check refreshToken is used/revoked?
            if (storedToken.IsUsed)
            {
                return new ApiResponse
                {
                    Message = Message.REFRESH_TOKEN_IS_USED.ToString()
                };
            }
            if (storedToken.IsRevoked)
            {
                return new ApiResponse
                {
                    Message = Message.REFRESH_TOKEN_IS_REVOKED.ToString()
                };
            }

            //check 6: AccessToken id == JwtId in RefreshToken
            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedToken.JwtId != jti)
            {
                return new ApiResponse
                {
                    Message = Message.TOKEN_NOT_MATCH.ToString()
                };
            }

            //Update token is used
            storedToken.IsRevoked = true;
            storedToken.IsUsed = true;
            _context.Update(storedToken);
            await _context.SaveChangesAsync();

            //create new token
            var user = await _context.Users.SingleOrDefaultAsync(nd => nd.Id == storedToken.UserId);
            var token = jwt.GenerateToken(user);
            cookie.SetaccessToken(token);

            return new ApiResponse
            {
                Message = Message.CREATE_SUCCESS.ToString(),
                Data = token,
                NumberOfRecords = 1
            };
        }
    }
}

