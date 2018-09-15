using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.RolePermission
{
    public class RelationRolePermissionViewModelBuilder : ViewModelBuilder<RelationRolePermissionViewModel, RelationRolePermissionData>
    {
        public RelationRolePermissionViewModelBuilder(RelationRolePermissionData model) : base(model)
        {
            viewModel = new RelationRolePermissionViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.RoleId = model.RoleId;
            viewModel.PermissionId = model.PermissionId;
        }
    }
}