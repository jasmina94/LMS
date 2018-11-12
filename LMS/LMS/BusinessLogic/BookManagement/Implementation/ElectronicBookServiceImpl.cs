using iTextSharp.text.pdf;
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

        public BookViewModel LoadBaseFromFile(string filePath, string fileName)
        {
            var viewModel = new BookViewModel();
            PdfReader reader = new PdfReader(filePath);

            viewModel.UploadSuccess = true;
            viewModel.Author = GetValue(reader, "Author");
            viewModel.Title = GetValue(reader, "Title");
            viewModel.Keywords = GetValue(reader, "Keywords");
            viewModel.Filename = fileName;
            viewModel.MIME = "application/pdf";

            return viewModel;
        }

        private string GetValue(PdfReader reader, string fieldName)
        {
            string value;

            if (!reader.Info.TryGetValue(fieldName, out value))
            {
                value = string.Empty;
            }

            return value;
        }
    }
}