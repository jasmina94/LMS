using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.User;

namespace LMS.Infrastructure.ModelBuilders.Implementation.User
{
    public class UserViewModelBuilder : ViewModelBuilder<UserViewModel, UserData>
    {
        public ICategoryRepository CategoryRepository { get; set; }

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
    }
}