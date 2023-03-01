using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Models
{
    public class AuthenticationRequest
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(35)]
        public string Password { get; set; }    
    }
}
