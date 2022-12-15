using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BTP_API.Ultils
{
    public class Jwt
    {
        private readonly AppSettings _appSettings;
        private readonly BTPContext _context;
        private readonly IConfiguration _config;

        public Jwt(BTPContext context, AppSettings appSettings, IConfiguration config)
        {
            _context = context;
            _appSettings = appSettings;
            _config = config;
        }
        public TokenModel GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Name, user.Fullname),
                        new Claim("Phone", user.Phone),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     //Roles
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
           

            _context.SaveChanges();

            return new TokenModel
            {
                AccessToken = accessToken
            };
        }
        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        
    }
}
