using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Models;

namespace EducationPortal.ViewModels
{
    public class HomeViewModel
    {
        public List<Image> Images { get; set; }
        public List<Course> Courses { get; set; }
        public SignInViewModel signInViewModel = new SignInViewModel();
        public RegisterViewModel registerViewModel = new RegisterViewModel();
        public ContactUsViewModel contactUsViewModel = new ContactUsViewModel();

        public class SignInViewModel
        {
            [Required(ErrorMessage = "Login can't be empty")]
            [DataType(DataType.Text)]
            public string Login { get; set; }

            [Required(ErrorMessage = "Password can't be empty")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class RegisterViewModel : SignInViewModel
        {
            [Required(ErrorMessage = "Password")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Confirm password doesn't match")]
            public string PasswordCheck { get; set; }
        }

        public class ContactUsViewModel
        {
            [Required(ErrorMessage = "We should reply somewhere")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "You should write something to us")]
            [DataType(DataType.MultilineText)]
            public string Text { get; set; }
        }
    }
}