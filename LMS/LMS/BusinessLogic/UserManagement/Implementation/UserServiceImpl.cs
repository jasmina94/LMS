using System.Collections.Generic;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.Models.ViewModels.User;
using LMS.Infrastructure.ModelBuilders.Implementation.User;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.DomainModel.DomainObject.Relation;
using LMS.BusinessLogic.RoleManagement.Interfaces;
using System;
using LMS.Models.ViewModels.Relation;

namespace LMS.BusinessLogic.LanguageManagement.Implementations
{
    public class UserServiceImpl : IUserService
    {
        public IUserRepository UserRepository { get; set; }

        public IRelationUserBookCopyRepository RelationUserBookCopyRepository { get; set; }

        public IRoleService RoleService { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public UserViewModel Get(int? userId)
        {
            var viewModel = new UserViewModel();
            if (userId.HasValue)
            {
                UserData domainModel = UserRepository.GetDataById(userId.Value);
                UserViewModelBuilder builder = BuilderResolverService.Get<UserViewModelBuilder, UserData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<UserViewModel> GetAll(bool active)
        {
            var viewModels = new List<UserViewModel>();
            var domainModels = new List<UserData>();

            domainModels = active ? UserRepository.GetAllActiveData() : UserRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        private List<UserViewModel> ConvertDataToViewModels(List<UserData> domainModels)
        {
            var viewModels = new List<UserViewModel>();

            foreach (var item in domainModels)
            {
                UserViewModelBuilder builder = BuilderResolverService.Get<UserViewModelBuilder, UserData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        public SaveUserResult Save(UserViewModel viewModel)
        {
            var result = new SaveUserResult();

            UserDomainModelBuilder builder = BuilderResolverService.Get<UserDomainModelBuilder, UserViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            UserData domainModel = builder.GetDataModel();

            int id = UserRepository.SaveData(domainModel);
            if (id != 0)
            {
                if(viewModel.RoleId != 0)
                {
                    //Save relation user - role
                    var relationUserRoleViewModel = CreateUserRoleViewModel(id, viewModel.RoleId, 0);
                    RoleService.SaveRelationUserRole(relationUserRoleViewModel);
                }
                result = new SaveUserResult(id, domainModel.FullFirstAndLastName);
            }

            return result;
        }

        private RelationUserRoleViewModel CreateUserRoleViewModel(int userId, int roleId, int userCreatedId)
        {
            var relation = new RelationUserRoleViewModel()
            {
                RoleId = roleId,
                UserId = userId,
                UserCreatedById = userCreatedId
            };

            return relation;
        }

        public DeleteUserResult Delete(int? userId)
        {
            var result = new DeleteUserResult();
            if (userId.HasValue)
            {
                UserData domainModel = UserRepository.GetDataById(userId.Value);
                if (domainModel != null)
                {
                    List<RelationUserBookCopyData> loans = RelationUserBookCopyRepository.GetLoansForUser(userId.Value);
                    if(loans.Count == 0)
                    {
                        UserRepository.DeleteById(userId.Value);
                        result = new DeleteUserResult(userId.Value, domainModel.FullFirstAndLastName);
                    }
                    else
                    {
                        result.Message = "Can't delete this user. There are some not returned loans.";
                    }                    
                }
            }

            return result;
        }
    }
}