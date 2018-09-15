using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Models.ViewModels;

namespace LMS.Infrastructure.ModelConstructor.Implementation
{
    public class ModelConstructor : IModelConstructor
    {
        public void ConstructDomainModelData<T1, T2>(DomainModelBuilder<T1, T2> domainModelBuilder)
            where T1 : BaseData
            where T2 : ViewModel
        {
            domainModelBuilder.BuildBaseData();
            domainModelBuilder.BuildConcreteData();
        }

        public void ConstructViewModelData<T1, T2>(ViewModelBuilder<T1, T2> viewModelBuilder)
            where T1 : ViewModel
            where T2 : BaseData
        {
            viewModelBuilder.BuildViewModelData();
            viewModelBuilder.BuildViewModelConcreteData();
        }
    }
}