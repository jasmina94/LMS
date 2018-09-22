using System.Collections.Generic;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Models.ViewModels.Book;
using LMS.Infrastructure.ModelBuilders.Implementation.Book;
using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;

namespace LMS.BusinessLogic.BookManagement.Implementations
{
    public class BookServiceImpl : IBookService
    {
        public IBookRepository BookRepository { get; set; }

        public IBookCopyRepository BookCopyRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public BookViewModel Get(int? userId)
        {
            var viewModel = new BookViewModel();
            if (userId.HasValue)
            {
                BookData domainModel = BookRepository.GetDataById(userId.Value);
                BookViewModelBuilder builder = BuilderResolverService.Get<BookViewModelBuilder, BookData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<BookViewModel> GetAll(bool active)
        {
            var viewModels = new List<BookViewModel>();
            var domainModels = new List<BookData>();

            domainModels = active ? BookRepository.GetAllActiveData() : BookRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        private List<BookViewModel> ConvertDataToViewModels(List<BookData> domainModels)
        {
            var viewModels = new List<BookViewModel>();

            foreach (var item in domainModels)
            {
                BookViewModelBuilder builder = BuilderResolverService.Get<BookViewModelBuilder, BookData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        public SaveBookResult Save(BookViewModel viewModel)
        {
            var result = new SaveBookResult();

            BookDomainModelBuilder builder = BuilderResolverService.Get<BookDomainModelBuilder, BookViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            BookData domainModel = builder.GetDataModel();

            int id = BookRepository.SaveData(domainModel);
            if (id != 0)
            {
                result = new SaveBookResult(id, domainModel.BookAuthorAndTitle);
            }

            return result;
        }

        public SaveBookResult Save(BookCopyViewModel viewModel)
        {
            var result = new SaveBookResult();

            viewModel.Id = 0;  //Always create never update

            BookCopyDomainModelBuilder builder = BuilderResolverService.Get<BookCopyDomainModelBuilder, BookCopyViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            BookCopyData domainModel = builder.GetDataModel();

            int id = BookCopyRepository.SaveData(domainModel);
            if (id != 0)
            {
                BookViewModel bookViewModel = Get(viewModel.BookId);
                bookViewModel.NumOfAvailableCopies = bookViewModel.NumOfAvailableCopies + 1;
                SaveBookResult updateResult = Save(bookViewModel);

                if (updateResult.Success)
                {
                    result = new SaveBookResult(id, viewModel.BookAuthorAndTitle, 
                        "Successfully added copy of book " + viewModel.BookAuthorAndTitle);
                }
            }

            return result;
        }

        public DeleteBookResult Delete(int? bookId)
        {
            var result = new DeleteBookResult();
            if (bookId.HasValue)
            {
                BookData domainModel = BookRepository.GetDataById(bookId.Value);
                if (domainModel != null)
                {
                    BookRepository.DeleteById(bookId.Value);
                    result = new DeleteBookResult(bookId.Value, domainModel.BookAuthorAndTitle);
                }
            }

            return result;
        }
    }
}