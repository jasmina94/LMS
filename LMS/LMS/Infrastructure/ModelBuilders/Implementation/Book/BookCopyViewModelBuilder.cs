using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Book;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Book
{
    public class BookCopyViewModelBuilder : ViewModelBuilder<BookCopyViewModel, BookCopyData>
    {
        public IBookRepository BookRepository { get; set; }

        public BookCopyViewModelBuilder(BookCopyData model) : base(model)
        {
            viewModel = new BookCopyViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.BookId = model.BookId;
            viewModel.BookAuthorAndTitle = GetTitleAndAuthor(model.BookId);
        }

        private string GetTitleAndAuthor(int bookId)
        {
            string titleAndAuthor;
            BookData book = BookRepository.GetDataById(bookId);
            titleAndAuthor = book.BookAuthorAndTitle;

            return titleAndAuthor;
        }
    }
}