using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Role
{
    public class RoleViewModel : ViewModel
    {
        [RequiredProperty("Role name is required!")]
        public string Name { get; set; }

        [RequiredProperty("Role code is required!")]
        public string Code { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}