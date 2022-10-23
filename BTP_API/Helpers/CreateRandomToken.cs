using System.Security.Cryptography;

namespace BTP_API.Helpers
{
    public class CreateRandomToken
    {
        public string CreateRandomToken64Byte()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        public string CreateRandomCode4Byte()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
        }
    }
}
