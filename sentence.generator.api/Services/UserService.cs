using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sentence.generator.api.Data;
using sentence.generator.api.IServices;
using sentence.generator.api.RequestModel;
using sentence.generator.api.ResponseModel;

namespace sentence.generator.api.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        public UserService(ApplicationDbContext dbContext, ITokenService userService, UserManager<ApplicationUser> userManager)
        {
            this._context = dbContext;
            this.userManager = userManager;
            this._tokenService = userService;
        }
        public async Task<TokenResponse> LoginAsync(LoginCredentials loginRequest)
        {
            var user = _context.Users.SingleOrDefault(user => user.Email == loginRequest.Username);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Email not found",
                    ErrorCode = "L02"
                };
            }

            user = await ValidateUser(loginRequest);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Invalid Password",
                    ErrorCode = "L03"
                };
            }

            var token = await Task.Run(() => _tokenService.GenerateTokensAsync(user.Id));

            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2,
                UserId = user.Id
            };
        }

        public async Task<LogoutResponse> LogoutAsync(string userId)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == userId);

            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }

            _context.RefreshTokens.Remove(refreshToken);

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }

            return new LogoutResponse { Success = false, Error = "Unable to logout user", ErrorCode = "L04" };
        }

        public async Task<RegisterResponse> SignupAsync(UserDetails signupRequest)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(user => user.Email == signupRequest.Email);

            if (existingUser != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Error = "User already exists with the same email",
                    ErrorCode = "S02"
                };
            }

            if (signupRequest.Password.Length <= 7) // This can be more complicated than only length, you can check on alphanumeric and or special characters
            {
                return new RegisterResponse
                {
                    Success = false,
                    Error = "Password is weak",
                    ErrorCode = "S04"
                };
            }

            var user = new ApplicationUser() { UserName = signupRequest.Email, Email = signupRequest.Email, FirstName = signupRequest.FirstName, LastName = signupRequest.LastName };
            var result = await userManager.CreateAsync(user, signupRequest.Password);


            if (result.Succeeded)
            {
                return new RegisterResponse { Success = true, Email = user.Email };
            }

            return new RegisterResponse
            {
                Success = false,
                Error = "Unable to save the user",
                ErrorCode = "S05"
            };
        }

        private async Task<ApplicationUser> ValidateUser(LoginCredentials credentials)
        {
            var ApplicationUser = await userManager.FindByNameAsync(credentials.Username);
            if (ApplicationUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(ApplicationUser, ApplicationUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : ApplicationUser;
            }

            return null;
        }
    }
}
