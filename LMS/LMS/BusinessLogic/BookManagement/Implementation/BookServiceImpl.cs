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

        public BookViewModel Get(int? bookId)
        {
            var viewModel = new BookViewModel();
            if (bookId.HasValue)
            {
                BookData domainModel = BookRepository.GetDataById(bookId.Value);
                BookViewModelBuilder builder = BuilderResolverService.Get<BookViewModelBuilder, BookData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public BookCopyViewModel GetCopy(int? bookCopyId)
        {
            var viewModel = new BookCopyViewModel();
            if (bookCopyId.HasValue)
            {
                BookCopyData domainModel = BookCopyRepository.GetDataById(bookCopyId.Value);
                BookCopyViewModelBuilder builder = BuilderResolverService.Get<BookCopyViewModelBuilder, BookCopyData>(domainModel);
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

        public List<BookCopyViewModel> GetAllCopies(bool active)
        {
            var viewModels = new List<BookCopyViewModel>();
            var domainModels = new List<BookCopyData>();

            domainModels = active ? BookCopyRepository.GetAllActiveData() : BookCopyRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        public List<BookCopyViewModel> GetAvailableCopies()
        {
            var viewModels = new List<BookCopyViewModel>();
            var domainModels = new List<BookCopyData>();

            domainModels = BookCopyRepository.GetAvailableCopies();
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

        private List<BookCopyViewModel> ConvertDataToViewModels(List<BookCopyData> domainModels)
        {
            var viewModels = new List<BookCopyViewModel>();

            foreach (var item in domainModels)
            {
                BookCopyViewModelBuilder builder = BuilderResolverService.Get<BookCopyViewModelBuilder, BookCopyData>(item);
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

        public SaveBookResult SaveOnly(BookCopyViewModel viewModel)
        {
            var result = new SaveBookResult();
            BookCopyDomainModelBuilder builder = BuilderResolverService.Get<BookCopyDomainModelBuilder, BookCopyViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            BookCopyData domainModel = builder.GetDataModel();

            int id = BookCopyRepository.SaveData(domainModel);
            if (id != 0)
            {
                result = new SaveBookResult(id, domainModel.BookId.ToString());
            }

            return result;
        }

        public DeleteBookResult Delete(int? bookId)
        {
            var result = new DeleteBookResult();
            if (bookId.HasValue)
            {
                DeleteBookResult deletingCopies = DeleteCopiesByBook(bookId.Value);
                if (deletingCopies.Success)
                {
                    BookData domainModel = BookRepository.GetDataById(bookId.Value);
                    if (domainModel != null)
                    {
                        BookRepository.DeleteById(bookId.Value);
                        result = new DeleteBookResult(bookId.Value, domainModel.BookAuthorAndTitle);
                    }
                }                
            }

            return result;
        }

        public DeleteBookResult DeleteCopy(int bookCopyId)
        {
            var result = new DeleteBookResult();
            BookCopyData bookCopyData = BookCopyRepository.GetDataById(bookCopyId);
            if (bookCopyData != null)
            {
                List<BookCopyData> list = new List<BookCopyData>() { bookCopyData };
                if (CheckIfAnyCopyIsBorrowed(list))
                {
                    result.Message = "Book copy is currently borrowed. Can't be deleted.";
                }
                else
                {
                    BookCopyRepository.DeleteById(bookCopyData.Id);

                    BookData book = BookRepository.GetDataById(bookCopyData.BookId);
                    book.NumOfAvailableCopies = book.NumOfAvailableCopies - 1;
                    BookRepository.SaveData(book);

                    result = new DeleteBookResult(book.Id, book.BookAuthorAndTitle, bookCopyData.Id);
                }
            }
            else
            {
                result.Message = "Please select book copy for delete.";
            }

            return result;
        }

        public DeleteBookResult DeleteCopiesByBook(int refBookId)
        {
            var result = new DeleteBookResult();
            List<BookCopyData> bookCopies = BookCopyRepository.GetCopiesForBook(refBookId);
            if(bookCopies != null && bookCopies.Count != 0)
            {
                if (CheckIfAnyCopyIsBorrowed(bookCopies))
                {
                    result.Message = "Book copy is currently borrowed. Can't be deleted.";
                }
                else
                {
                    DeleteEachBookCopy(bookCopies);
                    result.Success = true;
                }
                    
            }

            return result;
        }

        private bool CheckIfAnyCopyIsBorrowed(List<BookCopyData> bookCopies)
        {
            bool hasBorrowedCopy = false;
            foreach (var item in bookCopies)
            {
                if (item.OnLoan)
                {
                    hasBorrowedCopy = true;
                    break;
                }
            }

            return hasBorrowedCopy;
        }

        private void DeleteEachBookCopy(List<BookCopyData> bookCopies)
        {
            foreach (var item in bookCopies)
            {
                BookCopyRepository.DeleteById(item.Id);
            }
        }
    }
}