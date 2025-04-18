using Microsoft.AspNetCore.Identity;

namespace LocalizeApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Password { get; set; } = string.Empty;
    }
}
