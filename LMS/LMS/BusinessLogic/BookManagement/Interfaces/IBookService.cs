using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface IBookService
    {
        BookViewModel Get(int? bookId);

        BookCopyViewModel GetCopy(int? bookCopyId);

        List<BookViewModel> GetAll(bool active);

        List<BookCopyViewModel> GetAllCopies(bool active);

        List<BookCopyViewModel> GetAvailableCopies();

        SaveBookResult Save(BookViewModel viewModel, UserSessionObject user);

        SaveBookResult Save(BookCopyViewModel viewModel, UserSessionObject user);

        SaveBookResult SaveOnly(BookCopyViewModel viewModel, UserSessionObject user);

        DeleteBookResult Delete(int? bookId, UserSessionObject user);

        DeleteBookResult DeleteCopy(int bookCopyId, UserSessionObject user);
    }
}
