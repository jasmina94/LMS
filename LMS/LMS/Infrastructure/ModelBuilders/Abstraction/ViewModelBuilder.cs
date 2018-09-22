using Autofac;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.Models.ViewModels;

namespace LMS.Infrastructure.ModelBuilders.Abstraction
{
    public abstract class ViewModelBuilder<T1, T2> where T1 : ViewModel where T2 : BaseData
    {
        public IComponentContext ComponetContext { get; set; }

        protected T1 viewModel;
        protected T2 model;

        public ViewModelBuilder(T2 model)
        {
            this.model = model;
        }

        public void BuildViewModelData()
        {
            viewModel.Id = model.Id;
            viewModel.IsActive = model.IsActive;
            viewModel.UserCreatedById = model.RefUserCreatedBy;
            viewModel.UserCreatedBy = GetNameForUser(model.RefUserCreatedBy);
            viewModel.DateTimeCreatedOn = model.DateTimeCreatedOn;
            viewModel.UserDeletedBy = GetNameForUser(model.RefUserCreatedBy);
            viewModel.UserDeletedById = model.RefUserDeletedBy;
            viewModel.DateTimeDeletedOn = model.DateTimeDeletedOn;
        }        

        public abstract void BuildViewModelConcreteData();

        public T1 GetViewModel()
        {
            return viewModel;
        }

        private string GetNameForUser(int userId)
        {
            string name = string.Empty;
            if (userId != 0)
            {
                var userRepository = ComponetContext.Resolve<IUserRepository>();
                UserData user = userRepository.GetDataById(userId);
                name = user.Username;
            }

            return name;
        }
    }
}