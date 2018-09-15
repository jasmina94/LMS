using LMS.DomainModel.DomainObject;
using LMS.Models.ViewModels;

namespace LMS.Infrastructure.ModelBuilders.Abstraction
{
    public abstract class ViewModelBuilder<T1, T2> where T1 : ViewModel where T2 : BaseData
    {
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
            viewModel.RefUserCreatedBy = model.RefUserCreatedBy;
            viewModel.DateTimeCreatedOn = model.DateTimeCreatedOn;
            viewModel.RefUserDeletedBy = model.RefUserDeletedBy;
            viewModel.DateTimeDeletedOn = model.DateTimeDeletedOn;
        }

        public abstract void BuildViewModelConcreteData();

        public T1 GetViewModel()
        {
            return viewModel;
        }
    }
}