using System.ComponentModel.DataAnnotations;

namespace Bank3Tier.Api.Resources.Auth
{
    public class LoginUserResource
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
