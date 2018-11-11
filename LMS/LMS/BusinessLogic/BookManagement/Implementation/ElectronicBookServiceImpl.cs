using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using System.Linq;

namespace LMS.BusinessLogic.BookManagement.Implementation
{
    public class ElectronicBookServiceImpl : IElectronicBookService
    {
        public IBookRepository BookRepository { get; set; }

        public IBookService BookService { get; set; }

        public BookViewModel Get(int? bookId)
        {
            return BookService.Get(bookId);
        }

        public List<BookViewModel> GetAll(bool active)
        {
            var viewModels = BookService.GetAll(active);

            return viewModels.Where(x => x.IsElectronic).ToList();
        }
    }
}