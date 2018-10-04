using LMS.Infrastructure.Attributes.Implementation;

namespace LMS.Models.ViewModels.Account
{
    public class PasswordChangeViewModel
    {
        [RequiredProperty("Old password is required!")]
        public string OldPassword { get; set; }

        [RequiredProperty("New password is required!")]
        public string NewPassword { get; set; }

        [RequiredProperty("Repeat password is required!")]
        public string RepeatPassword { get; set; }
    }
}