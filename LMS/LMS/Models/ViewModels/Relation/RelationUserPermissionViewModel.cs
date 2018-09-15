using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Relation
{
    public class RelationUserPermissionViewModel : ViewModel
    {
        public int UserId { get; set; }

        public int PermissionId { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}