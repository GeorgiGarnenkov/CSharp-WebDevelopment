using System.ComponentModel.DataAnnotations;

namespace Chushka.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Username length must be between {2} and {1}", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Password length must be between {2} and {1}", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Confirm Password length must be between {2} and {1}", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        [RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Please enter valid Email address.")]
        public string Email { get; set; }
    }
}
