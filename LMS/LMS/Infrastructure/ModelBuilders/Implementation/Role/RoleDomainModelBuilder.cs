using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Role;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Role
{
    public class RoleDomainModelBuilder : DomainModelBuilder<RoleData, RoleViewModel>
    {
        public RoleDomainModelBuilder(RoleViewModel viewModel) : base(viewModel)
        {
            model = new RoleData();
        }

        public override void BuildConcreteData()
        {
            model.CodeRole = viewModel.Code;
            model.NameRole = viewModel.Name;
        }
    }
}