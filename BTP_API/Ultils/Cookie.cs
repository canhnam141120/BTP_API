using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BTP_API.Ultils
{
    public class Cookie : ControllerBase
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public Cookie()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId(string token)
        {
            //var cookie = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            if (token == null)
            {
                return 0;
            }
            var user = new JwtSecurityToken(jwtEncodedString: token);
            var userId = user.Claims.FirstOrDefault();
            int id = Int32.Parse(userId.Value);
            return id;
        }
        //public void SetaccessToken(TokenModel token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.Now.AddHours(1),
        //    };
        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", token.AccessToken, cookieOptions);
        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", token.RefreshToken, cookieOptions);
        //}
    }
}
