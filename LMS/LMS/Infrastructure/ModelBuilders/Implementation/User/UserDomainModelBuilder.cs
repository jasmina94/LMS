using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.User;

namespace LMS.Infrastructure.ModelBuilders.Implementation.User
{
    public class UserDomainModelBuilder : DomainModelBuilder<UserData, UserViewModel>
    {
        public UserDomainModelBuilder(UserViewModel viewModel) : base(viewModel)
        {
            model = new UserData();
        }

        public override void BuildConcreteData()
        {
            model.Firstname = viewModel.Firstname;
            model.Lastname = viewModel.Lastname;
            model.Username = viewModel.Username;
            model.Password = viewModel.UserPassword;
            model.BirthDate = viewModel.BirthDate;
            model.Email = viewModel.Email;
            model.CategoryId = viewModel.CategoryId;
        }
    }
}