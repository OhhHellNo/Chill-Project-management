using System.ComponentModel.DataAnnotations;

namespace Chill_Project_Management.ViewModels
{
    public class RegisterViewModel
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required, MustBeTrue(ErrorMessage = "You must accept the terms")]
        public bool AgreeTerms { get; set; }
    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) => value is true;
    }
}