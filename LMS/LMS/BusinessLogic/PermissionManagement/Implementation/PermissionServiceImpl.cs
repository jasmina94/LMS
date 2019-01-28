using LMS.Services.Interfaces;
using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.Models.ViewModels.Permission;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.BusinessLogic.PermissionManagement.Interface;
using LMS.DomainModel.Repository.Permission.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Permission;
using LMS.BusinessLogic.RoleManagement.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Linq;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.Authorization;
using LMS.BusinessLogic.PermissionManagement.Model;

namespace LMS.BusinessLogic.PermissionManagement.Implementation
{
    public class PermissionServiceImpl : IPermissionService
    {
        #region Injected

        public IPermissionRepository PermissionRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IRelationUserPermissionRepository RelationUserPermissionRepository { get; set; }

        public IRelationRolePermissionRepository RelationRolePermissionRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public IRoleService RoleService { get; set; }

        #endregion

        public PermissionViewModel Get(int? permissionId)
        {
            var viewModel = new PermissionViewModel();
            if (permissionId.HasValue)
            {
                PermissionData domainModel = PermissionRepository.GetDataById(permissionId.Value);
                PermissionViewModelBuilder builder = BuilderResolverService.Get<PermissionViewModelBuilder, PermissionData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<PermissionViewModel> GetAll(bool active)
        {
            var viewModels = new List<PermissionViewModel>();
            var domainModels = new List<PermissionData>();

            domainModels = active ? PermissionRepository.GetAllActiveData() : PermissionRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }  

        public AdministrationUserPermissionViewModel GetUserPermissions(int userId)
        {
            var viewModel = new AdministrationUserPermissionViewModel
            {
                Assigned = Get(userId, false),
                Available = Get(userId, true)
            };

            return viewModel;
        }

        public PermissionResult Assign(List<int> permissions, int userId, UserSessionObject currentUser)
        {
            bool flag = true;            
            var wrongIds = new List<int>();
            List<int> alreadyAssigned = RelationUserPermissionRepository
                                            .GetRelationUserPermissionFor(userId)
                                            .Select(x => x.PermissionId)
                                            .ToList();
            PermissionResult result = null;

            foreach (int permissionId in permissions)
            {
                if (!alreadyAssigned.Contains(permissionId))
                {
                    var newLink = new RelationUserPermissionData
                    {
                        PermissionId = permissionId,
                        UserId = userId,
                        RefUserCreatedBy = currentUser.UserId
                    };
                    RelationUserPermissionRepository.SaveData(newLink);
                }
                else
                {
                    flag = false;
                    wrongIds.Add(permissionId);
                }
            }

            if (!flag)
            {
                result = new PermissionResult(false, PermissionResult.OperationType.Assign, wrongIds);
            }
            else
            {
                result = new PermissionResult(true, PermissionResult.OperationType.Assign);
            }

            return result;
        }

        public PermissionResult Remove(List<int> permissions, int userId, UserSessionObject currentUser)
        {
            bool flag = true;
            var wrongIds = new List<int>();
            var result = new PermissionResult();            
            List<int> alreadyAssigned = RelationUserPermissionRepository
                                            .GetRelationUserPermissionFor(userId)
                                            .Select(x => x.PermissionId)
                                            .ToList();

            foreach (int permissionId in permissions)
            {
                if (!alreadyAssigned.Contains(permissionId))
                {
                    flag = false;
                    wrongIds.Add(permissionId);
                }
                else
                {
                    var data = RelationUserPermissionRepository.GetRelationUserPermissionFor(userId, permissionId);
                    RelationUserPermissionRepository.DeleteById(data.Id, currentUser.UserId);
                }
            }

            if (!flag)
            {
                result = new PermissionResult(false, PermissionResult.OperationType.Remove, wrongIds);
            }
            else
            {
                result = new PermissionResult(true, PermissionResult.OperationType.Remove);
            }


            return result;
        }

        #region Private

        private List<PermissionViewModel> Get(int userId, bool available)
        {
            var roleId = RoleService.GetRoleFor(userId).Id;

            List<int> rolesPermissionIds = RelationRolePermissionRepository.GetAllActiveData()
                .Where(x => x.RoleId == roleId)
                .Select(x => x.PermissionId)
                .ToList();

            List<int> userPermissionIds = RelationUserPermissionRepository.GetAllActiveData()
                .Where(x => x.UserId == userId)
                .Select(x => x.PermissionId)
                .ToList();

            var union = rolesPermissionIds.Union(userPermissionIds);

            var domainData = PermissionRepository.GetAllActiveData()
                .Where(x => available ? !union.Contains(x.Id) : union.Contains(x.Id))
                .ToList();

            return ConvertDataToViewModels(domainData).OrderByDescending(x => x.Id).ToList();
        }

        private List<PermissionViewModel> ConvertDataToViewModels(List<PermissionData> domainModels)
        {
            var viewModels = new List<PermissionViewModel>();

            foreach (var item in domainModels)
            {
                PermissionViewModelBuilder builder = BuilderResolverService.Get<PermissionViewModelBuilder, PermissionData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        #endregion
    }
}