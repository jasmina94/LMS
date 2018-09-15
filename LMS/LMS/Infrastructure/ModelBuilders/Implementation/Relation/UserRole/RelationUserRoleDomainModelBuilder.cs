using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserRole
{
    public class RelationUserRoleDomainModelBuilder : DomainModelBuilder<RelationUserRoleData, RelationUserRoleViewModel>
    {
        public RelationUserRoleDomainModelBuilder(RelationUserRoleViewModel viewModel) : base(viewModel)
        {
            model = new RelationUserRoleData();
        }

        public override void BuildConcreteData()
        {
            model.UserId = viewModel.UserId;
            model.RoleId = viewModel.RoleId;
        }
    }
}