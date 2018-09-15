using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserPermission
{
    public class RelationUserPermissionViewModelBuilder : ViewModelBuilder<RelationUserPermissionViewModel, RelationUserPermissionData>
    {
        public RelationUserPermissionViewModelBuilder(RelationUserPermissionData model) : base(model)
        {
            viewModel = new RelationUserPermissionViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.PermissionId = model.PermissionId;
            viewModel.UserId = model.UserId;
        }
    }
}