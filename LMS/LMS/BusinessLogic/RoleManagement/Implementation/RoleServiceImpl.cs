using LMS.BusinessLogic.RoleManagement.Interfaces;
using System.Collections.Generic;
using LMS.Models.ViewModels.Role;
using LMS.DomainModel.Repository.Role.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Implementation.Role;
using LMS.Models.ViewModels.Relation;
using System;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserRole;
using LMS.DomainModel.DomainObject.Relation;

namespace LMS.BusinessLogic.RoleManagement.Implementation
{
    public class RoleServiceImpl : IRoleService
    {
        public IRoleRepository RoleRepository { get; set; }

        public IRelationUserRoleRepository RelationUserRoleRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

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

        public void SaveRelationUserRole(RelationUserRoleViewModel viewModel)
        {
            RelationUserRoleDomainModelBuilder builder = BuilderResolverService.Get
                <RelationUserRoleDomainModelBuilder, RelationUserRoleViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            RelationUserRoleData domainModel = builder.GetDataModel();

            RelationUserRoleRepository.SaveData(domainModel);            
        }
    }
}