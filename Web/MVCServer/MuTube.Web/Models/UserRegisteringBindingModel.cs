using System.ComponentModel.DataAnnotations;

namespace MuTube.Web.Models
{
    public class UserRegisteringBindingModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
