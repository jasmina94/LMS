using LMS.DomainModel.DomainObject;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.DomainModel.Repository.Role.Interfaces;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.User;
using System.Collections.Generic;

namespace LMS.Infrastructure.ModelBuilders.Implementation.User
{
    public class UserViewModelBuilder : ViewModelBuilder<UserViewModel, UserData>
    {
        public ICategoryRepository CategoryRepository { get; set; }

        public IRelationUserRoleRepository RelationUserRoleRepository { get; set; }

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
            viewModel.BirthDateString = model.BirthDate.ToString("dd/MM/yyyy");
            viewModel.CategoryId = model.CategoryId;
            viewModel.Category = GetCategoryName(model.CategoryId);
            SetRoleToUser(viewModel, model.Id);
            SetRoleToUser(viewModel, model.Id);
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

        private void SetRoleToUser(UserViewModel viewModel, int userId)
        {
            List<RelationUserRoleData> userRoleDatas = RelationUserRoleRepository.GetRelationUserRoleFor(userId);
            if(userRoleDatas.Count != 0)
            {
                RelationUserRoleData userRoleData = userRoleDatas[0];
                RoleData role = RoleRepository.GetDataById(userRoleData.RoleId);
                if(role != null)
                {
                    viewModel.Role = role.NameRole;
                    viewModel.RoleId = role.Id;
                }
            }
        }
    }
}