using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [MinLength(3, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [RegularExpression("[a-zA-Z0-9-_.*~]+")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password fields do not match!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "UCN")]
        [StringLength(maximumLength: 10, ErrorMessage = "UCN must be exactly {1} characters long.", MinimumLength = 10)]
        public string UniqueCitizenNumber { get; set; }
    }
}
