using LMS.BusinessLogic.BookManagement.Model;
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

        SaveBookResult Save(BookViewModel viewModel);

        SaveBookResult Save(BookCopyViewModel viewModel);

        SaveBookResult SaveOnly(BookCopyViewModel viewModel);

        DeleteBookResult Delete(int? bookId);
    }
}
