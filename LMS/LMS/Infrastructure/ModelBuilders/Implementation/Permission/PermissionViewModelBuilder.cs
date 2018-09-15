using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Permission;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Permission
{
    public class PermissionViewModelBuilder : ViewModelBuilder<PermissionViewModel, PermissionData>
    {
        public PermissionViewModelBuilder(PermissionData model) : base(model)
        {
            viewModel = new PermissionViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.Code = model.CodePermission;
            viewModel.Name = model.NamePermission;
        }
    }
}