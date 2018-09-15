using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Relation
{
    public class RelationUserRoleViewModel : ViewModel
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}