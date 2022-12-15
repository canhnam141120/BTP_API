namespace BTP_API.Services
{
    public interface IUserRepository
    {
        public Task<ApiResponse> loginAsync(LoginVM loginVM);
        public Task<ApiMessage> logoutAsync(string token);
        public Task<ApiMessage> registerAsync(RegisterVM registerVM);
        public Task<ApiMessage> verifyAsync(string verifyCode);
        public Task<ApiMessage> forgotPasswordAsync(string email);
        public Task<ApiMessage> resetPasswordAsync(ResetPasswordVM resetPasswordVM);
    }
}
