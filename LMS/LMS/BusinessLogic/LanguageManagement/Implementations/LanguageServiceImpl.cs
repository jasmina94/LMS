using System.Collections.Generic;
using LMS.BusinessLogic.LanguageManagement.Interfaces;
using LMS.BusinessLogic.LanguageManagement.Model;
using LMS.Models.ViewModels.Language;
using LMS.DomainModel.Repository.Language.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Implementation.Language;

namespace LMS.BusinessLogic.LanguageManagement.Implementations
{
    public class LanguageServiceImpl : ILanguageService
    {
        public ILanguageRepository LanguageRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public LanguageViewModel Get(int? languageId)
        {
            var viewModel = new LanguageViewModel();
            if (languageId.HasValue)
            {
                LanguageData domainModel = LanguageRepository.GetDataById(languageId.Value);
                LanguageViewModelBuilder builder = BuilderResolverService.Get<LanguageViewModelBuilder, LanguageData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<LanguageViewModel> GetAll(bool active)
        {
            var viewModels = new List<LanguageViewModel>();
            var domainModels = new List<LanguageData>();

            domainModels = active ? LanguageRepository.GetAllActiveData() : LanguageRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        private List<LanguageViewModel> ConvertDataToViewModels(List<LanguageData> domainModels)
        {
            var viewModels = new List<LanguageViewModel>();

            foreach (var item in domainModels)
            {
                LanguageViewModelBuilder builder = BuilderResolverService.Get<LanguageViewModelBuilder, LanguageData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        public SaveLanguageResult Save(LanguageViewModel viewModel)
        {
            var result = new SaveLanguageResult();

            LanguageDomainModelBuilder builder = BuilderResolverService.Get<LanguageDomainModelBuilder, LanguageViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            LanguageData domainModel = builder.GetDataModel();

            int id = LanguageRepository.SaveData(domainModel);
            if (id != 0)
            {
                result = new SaveLanguageResult(id, domainModel.NameLanguage);
            }

            return result;
        }

        public DeleteLanguageResult Delete(int? languageId)
        {
            var result = new DeleteLanguageResult();
            if (languageId.HasValue)
            {
                LanguageData domainModel = LanguageRepository.GetDataById(languageId.Value);
                if (domainModel != null)
                {
                    LanguageRepository.DeleteById(languageId.Value);
                    result = new DeleteLanguageResult(languageId.Value, domainModel.NameLanguage);
                }
            }

            return result;
        }
    }
}