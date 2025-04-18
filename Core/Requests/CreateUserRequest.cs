using System.ComponentModel.DataAnnotations;

namespace Localiza.Core.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
