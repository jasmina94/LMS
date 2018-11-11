using LMS.Models.ViewModels.Book;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface IElectronicBookService
    {
        BookViewModel Get(int? bookId);

        List<BookViewModel> GetAll(bool isActive);
    }
}
