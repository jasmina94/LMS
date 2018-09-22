using LMS.BusinessLogic.BookManagement.Model;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface IBookService
    {
        BookViewModel Get(int? bookId);

        List<BookViewModel> GetAll(bool active);

        SaveBookResult Save(BookViewModel viewModel);

        DeleteBookResult Delete(int? bookId);
    }
}
