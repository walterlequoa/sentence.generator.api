using sentence.generator.api.RequestModel;
using sentence.generator.api.ResponseModel;

namespace sentence.generator.api.IServices
{
    public interface IUserService
    {
        Task<TokenResponse> LoginAsync(LoginCredentials loginRequest);
        Task<RegisterResponse> SignupAsync(UserDetails signupRequest);
        Task<LogoutResponse> LogoutAsync(string userId);
    }
}
