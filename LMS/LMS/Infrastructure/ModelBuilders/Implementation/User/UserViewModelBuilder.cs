using LMS.Models.ViewModels.User;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.DomainModel.Repository.Role.Interfaces;
using LMS.Infrastructure.ModelBuilders.Abstraction;

namespace LMS.Infrastructure.ModelBuilders.Implementation.User
{
    public class UserViewModelBuilder : ViewModelBuilder<UserViewModel, UserData>
    {
        public ICategoryRepository CategoryRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public UserViewModelBuilder(UserData model) : base(model)
        {
            viewModel = new UserViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.Firstname = model.Firstname;
            viewModel.Lastname = model.Lastname;
            viewModel.Username = model.Username;
            viewModel.UserPassword = model.Password;
            viewModel.Email = model.Email;
            viewModel.BirthDate = model.BirthDate;
            viewModel.CategoryId = model.CategoryId;
            viewModel.Category = GetCategoryName(model.CategoryId);
            viewModel.RoleId = model.RoleId;
            viewModel.Role = GetRoleName(model.RoleId);
        }

        private string GetCategoryName(int categoryId)
        {
            string name = string.Empty;
            if(categoryId != 0)
            {
                CategoryData category = CategoryRepository.GetDataById(categoryId);
                name = category.NameCategory;
            }

            return name;
        }

        private string GetRoleName(int roleId)
        {
            return RoleRepository.GetDataById(roleId).NameRole;
        }
    }
}