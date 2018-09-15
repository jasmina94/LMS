using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserRole
{
    public class RelationUserRoleViewModelBuilder : ViewModelBuilder<RelationUserRoleViewModel, RelationUserRoleData>
    {
        public RelationUserRoleViewModelBuilder(RelationUserRoleData model) : base(model)
        {
            viewModel = new RelationUserRoleViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.RoleId = model.RoleId;
            viewModel.UserId = model.UserId;
        }
    }
}