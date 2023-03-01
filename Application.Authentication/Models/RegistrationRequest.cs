using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Models
{
    public class RegistrationRequest : AuthenticationRequest
    {
        [Required]
        [MaxLength(35)]
        public string ConfirmedPassword { get; set; }
    }
}
