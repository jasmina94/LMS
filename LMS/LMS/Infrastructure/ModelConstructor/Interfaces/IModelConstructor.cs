using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels;

namespace LMS.Infrastructure.ModelConstructor.Interfaces
{
    public interface IModelConstructor
    {
        void ConstructViewModelData<T1, T2>(ViewModelBuilder<T1, T2> viewModelBuilder)
         where T1 : ViewModel
         where T2 : BaseData;

        void ConstructDomainModelData<T1, T2>(DomainModelBuilder<T1, T2> domainModelBuilder)
           where T1 : BaseData
           where T2 : ViewModel;
    }
}
