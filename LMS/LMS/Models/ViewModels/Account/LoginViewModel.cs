using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Account
{ 
    public class LoginViewModel : ViewModel
    {
        [RequiredProperty("Username is required!")]
        public string Username { get; set; }

        [RequiredProperty("Password is required!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}