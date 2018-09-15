using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserPermission
{
    public class RelationUserPermissionDomainModelBuilder : DomainModelBuilder<RelationUserPermissionData, RelationUserPermissionViewModel>
    {
        public RelationUserPermissionDomainModelBuilder(RelationUserPermissionViewModel viewModel) : base(viewModel)
        {
            model = new RelationUserPermissionData();
        }

        public override void BuildConcreteData()
        {
            model.UserId = viewModel.UserId;
            model.PermissionId = viewModel.PermissionId;
        }
    }
}