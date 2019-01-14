using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.User;
using System;
using System.Globalization;

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
            model.BirthDate = DateTime.ParseExact(viewModel.BirthDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.Email = viewModel.Email;
            model.CategoryId = viewModel.CategoryId;
        }
    }
}