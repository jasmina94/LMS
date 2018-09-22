using System.Collections.Generic;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.Models.ViewModels.User;
using LMS.Infrastructure.ModelBuilders.Implementation.User;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.BusinessLogic.UserManagement.Interfaces;

namespace LMS.BusinessLogic.LanguageManagement.Implementations
{
    public class UserServiceImpl : IUserService
    {
        public IUserRepository UserRepository { get; set; }

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
                result = new SaveUserResult(id, domainModel.FullFirstAndLastName);
            }

            return result;
        }

        public DeleteUserResult Delete(int? userId)
        {
            var result = new DeleteUserResult();
            if (userId.HasValue)
            {
                UserData domainModel = UserRepository.GetDataById(userId.Value);
                if (domainModel != null)
                {
                    UserRepository.DeleteById(userId.Value);
                    result = new DeleteUserResult(userId.Value, domainModel.FullFirstAndLastName);
                }
            }

            return result;
        }
    }
}