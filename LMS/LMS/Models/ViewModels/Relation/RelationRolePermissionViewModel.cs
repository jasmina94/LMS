using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Relation
{
    public class RelationRolePermissionViewModel : ViewModel
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}