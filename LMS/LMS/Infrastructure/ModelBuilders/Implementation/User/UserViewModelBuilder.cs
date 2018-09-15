using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.User;

namespace LMS.Infrastructure.ModelBuilders.Implementation.User
{
    public class UserViewModelBuilder : ViewModelBuilder<UserViewModel, UserData>
    {
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
        }
    }
}