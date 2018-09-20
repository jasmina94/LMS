using LMS.DomainModel.DomainObject;
using LMS.Models.ViewModels;

namespace LMS.Infrastructure.ModelBuilders.Abstraction
{
    public abstract class DomainModelBuilder<T1, T2> where T1 : BaseData where T2 : ViewModel
    {
        protected T1 model;
        protected T2 viewModel;

        public DomainModelBuilder(T2 viewModel)
        {
            this.viewModel = viewModel;
        }

        public void BuildBaseData()
        {
            model.Id = viewModel.Id;
            model.IsActive = viewModel.IsActive;
            model.RefUserCreatedBy = viewModel.UserCreatedByInt;
            model.DateTimeCreatedOn = viewModel.DateTimeCreatedOn;
            model.RefUserDeletedBy = viewModel.UserDeletedById;
            model.DateTimeDeletedOn = viewModel.DateTimeDeletedOn;
        }

        public abstract void BuildConcreteData();

        public T1 GetDataModel()
        {
            return model;
        }
    }
}