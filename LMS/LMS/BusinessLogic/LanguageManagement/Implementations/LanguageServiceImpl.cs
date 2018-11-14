using LMS.Infrastructure.ModelBuilders.Implementation.Language;
using LMS.BusinessLogic.LanguageManagement.Interfaces;
using LMS.DomainModel.Repository.Language.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.BusinessLogic.LanguageManagement.Model;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Language;
using LMS.DomainModel.DomainObject;
using System.Collections.Generic;
using LMS.Services.Interfaces;


namespace LMS.BusinessLogic.LanguageManagement.Implementations
{
    public class LanguageServiceImpl : ILanguageService
    {
        public ILanguageRepository LanguageRepository { get; set; }

        public IBookRepository BookRepository { get; set; }

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

        public SaveLanguageResult Save(LanguageViewModel viewModel, UserSessionObject user)
        {
            var result = new SaveLanguageResult();

            LanguageDomainModelBuilder builder = BuilderResolverService.Get<LanguageDomainModelBuilder, LanguageViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            LanguageData domainModel = builder.GetDataModel();

            if (viewModel.Id == 0)            
                domainModel.RefUserCreatedBy = user.UserId;

            int id = LanguageRepository.SaveData(domainModel);
            if (id != 0)
                result = new SaveLanguageResult(id, domainModel.NameLanguage);

            return result;
        }

        public DeleteLanguageResult Delete(int? languageId, UserSessionObject user)
        {
            var result = new DeleteLanguageResult();
            if (languageId.HasValue)
            {
                if (!CheckReferencingBooks(languageId.Value))
                {
                    LanguageData domainModel = LanguageRepository.GetDataById(languageId.Value);
                    if (domainModel != null)
                    {
                        LanguageRepository.DeleteById(languageId.Value, user.UserId);
                        result = new DeleteLanguageResult(languageId.Value, domainModel.NameLanguage);
                    }
                }
                else
                {
                    result.Message = "This language can't be deleted. There are books connected to this language.";
                }
                
            }

            return result;
        }

        private bool CheckReferencingBooks(int languageId)
        {
            bool exists = false;
            List<BookData> books = BookRepository.GetBooksByLanguage(languageId);
            if (books != null && books.Count != 0)
            {
                exists = true;
            }

            return exists;
        }
    }
}