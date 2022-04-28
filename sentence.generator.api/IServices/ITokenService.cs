using sentence.generator.api.RequestModel;
using sentence.generator.api.RequestModel;
using sentence.generator.api.ResponseModel;

namespace sentence.generator.api.IServices
{
    public interface ITokenService
    {
        Task<Tuple<string, string, string>> GenerateTokensAsync(string userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(ApplicationUser user);
    }
}
