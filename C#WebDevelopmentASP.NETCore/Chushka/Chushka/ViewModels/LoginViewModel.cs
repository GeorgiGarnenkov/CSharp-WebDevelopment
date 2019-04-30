using System.ComponentModel.DataAnnotations;

namespace Chushka.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Username length must be between 5 and 30", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Password length must be between {0} and {1}", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
