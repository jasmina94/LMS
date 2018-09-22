using System;
using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Book;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.DomainModel.Repository.Language.Interfaces;
using LMS.DomainModel.Repository.Book.Interfaces;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Book
{
    public class BookViewModelBuilder : ViewModelBuilder<BookViewModel, BookData>
    {
        public IUserRepository UserRepository { get; set; }

        public ILanguageRepository LanguageRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }

        public BookViewModelBuilder(BookData model) : base(model)
        {
            viewModel = new BookViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.IsElectronic = model.IsElectronic;
            viewModel.IsElectronicLabel = model.IsElectronic ? "True" : "False";
            viewModel.NumOfAvailableCopies = model.NumOfAvailableCopies;
            viewModel.Title = model.Title;
            viewModel.Author = model.Author;
            viewModel.PublicationYear = model.PublicationYear;
            viewModel.Keywords = model.Keywords;
            viewModel.Filename = model.Filename;
            viewModel.MIME = model.MIME;
            viewModel.CategoryId = model.CategoryId;
            viewModel.Category = GetCategoryName(model.CategoryId);
            viewModel.LanguageId = model.LanguageId;
            viewModel.Language = GetLanguageName(model.LanguageId);
            viewModel.CataloguerId = model.CataloguerId;
            viewModel.Cataloguer = GetUserFullName(model.CataloguerId);
        }

        private string GetCategoryName(int categoryId)
        {
            string categoryName = string.Empty;           
            CategoryData category = CategoryRepository.GetDataById(categoryId);

            if(category != null)
                categoryName = category.NameCategory;

            return categoryName;
        }

        private string GetLanguageName(int categoryId)
        {
            string languageName = string.Empty;
            LanguageData language = LanguageRepository.GetDataById(categoryId);

            if (language != null)
                languageName = language.NameLanguage;

            return languageName;
        }

        private string GetUserFullName(int userId)
        {
            string fullname = string.Empty;
            UserData user = UserRepository.GetDataById(userId);

            if (user != null)
                fullname = user.FullFirstAndLastName;         

            return fullname;
        }
    }
}