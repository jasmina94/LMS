using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Permission;
using LMS.Models.ViewModels.Role;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Permission
{
    public class PermissionDomainModelBuilder : DomainModelBuilder<PermissionData, PermissionViewModel>
    {
        public PermissionDomainModelBuilder(PermissionViewModel viewModel) : base(viewModel)
        {
            model = new PermissionData();
        }

        public override void BuildConcreteData()
        {
            model.CodePermission = viewModel.Code;
            model.NamePermission = viewModel.Name;
        }
    }
}