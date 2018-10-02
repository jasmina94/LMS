namespace LMS.Models.ViewModels.Account
{
    public class PasswordChangeViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }
    }
}