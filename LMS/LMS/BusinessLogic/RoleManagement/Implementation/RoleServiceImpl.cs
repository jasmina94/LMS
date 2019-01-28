using LMS.Infrastructure.ModelBuilders.Implementation.Role;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.BusinessLogic.RoleManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.DomainModel.Repository.Role.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.Models.ViewModels.Role;
using System.Collections.Generic;
using LMS.Services.Interfaces;

namespace LMS.BusinessLogic.RoleManagement.Implementation
{
    public class RoleServiceImpl : IRoleService
    {
        public IRoleRepository RoleRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public IUserService UserService { get; set; }

        public RoleViewModel Get(int? roleId)
        {
            var viewModel = new RoleViewModel();
            if (roleId.HasValue)
            {
                RoleData domainModel = RoleRepository.GetDataById(roleId.Value);
                RoleViewModelBuilder builder = BuilderResolverService.Get<RoleViewModelBuilder, RoleData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<RoleViewModel> GetAll(bool active)
        {
            var viewModels = new List<RoleViewModel>();
            var domainModels = new List<RoleData>();

            domainModels = active ? RoleRepository.GetAllActiveData() : RoleRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }        

        public RoleViewModel GetRoleFor(int userId)
        {
            var roleId = UserService.Get(userId).RoleId;
            var role = RoleRepository.GetDataById(roleId);

            RoleViewModelBuilder builder = BuilderResolverService.Get<RoleViewModelBuilder, RoleData>(role);
            Constructor.ConstructViewModelData(builder);

            return builder.GetViewModel();
        }

        #region Private

        private List<RoleViewModel> ConvertDataToViewModels(List<RoleData> domainModels)
        {
            var viewModels = new List<RoleViewModel>();

            foreach (var item in domainModels)
            {
                RoleViewModelBuilder builder = BuilderResolverService.Get<RoleViewModelBuilder, RoleData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        #endregion
    }
}