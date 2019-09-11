using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Portfolio.Data.DataInitializer
{
    internal class PasswordHasher : PasswordHasher<IdentityUser>
    {
        public PasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor = null) : base(optionsAccessor)
        {
        }
    }
}