using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class AccountRegisterViewModel
    {
        // adds attributes above the properties for the purpose of controlling
        // the display (Display name), form validation (Required), and DataType
        // annotation (DataType - this content will appear as asterisks when
        // user types in the field on the form
        [Required, MaxLength(256), EmailAddress]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
