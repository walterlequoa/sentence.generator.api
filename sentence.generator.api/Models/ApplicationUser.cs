using Microsoft.AspNetCore.Identity;

namespace sentence.generator.api.RequestModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
