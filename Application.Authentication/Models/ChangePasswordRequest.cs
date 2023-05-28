using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Models
{
    public class ChangePasswordRequest : AuthenticationRequest
    {
        [Required]
        [MaxLength(35)]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(35)]
        [MinLength(8)]
        public string ConfirmedNewPassword { get; set; }
    }
}
