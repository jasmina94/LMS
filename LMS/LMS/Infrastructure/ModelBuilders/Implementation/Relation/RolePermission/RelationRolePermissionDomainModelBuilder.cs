using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.RolePermission
{
    public class RelationRolePermissionDomainModelBuilder : DomainModelBuilder<RelationRolePermissionData, RelationRolePermissionViewModel>
    {
        public RelationRolePermissionDomainModelBuilder(RelationRolePermissionViewModel viewModel) : base(viewModel)
        {
            model = new RelationRolePermissionData();
        }

        public override void BuildConcreteData()
        {
            model.PermissionId = viewModel.PermissionId;
            model.RoleId = viewModel.RoleId;
        }
    }
}