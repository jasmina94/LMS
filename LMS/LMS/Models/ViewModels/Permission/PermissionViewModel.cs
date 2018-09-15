using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Permission
{
    public class PermissionViewModel : ViewModel
    {
        [RequiredProperty("Permission name is required!")]
        public string Name { get; set; }

        [RequiredProperty("Permission code is required!")]
        public string Code { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}