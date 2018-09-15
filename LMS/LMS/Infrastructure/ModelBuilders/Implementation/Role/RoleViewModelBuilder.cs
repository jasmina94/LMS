using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Role;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Role
{
    public class RoleViewModelBuilder : ViewModelBuilder<RoleViewModel, RoleData>
    {
        public RoleViewModelBuilder(RoleData model) : base(model)
        {
            viewModel = new RoleViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.Code = model.CodeRole;
            viewModel.Name = model.NameRole;
        }
    }
}