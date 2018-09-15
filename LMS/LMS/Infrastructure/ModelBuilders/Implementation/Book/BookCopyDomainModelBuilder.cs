using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Book;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Book
{
    public class BookCopyDomainModelBuilder : DomainModelBuilder<BookCopyData, BookCopyViewModel>
    {
        public BookCopyDomainModelBuilder(BookCopyViewModel viewModel) : base(viewModel)
        {
            model = new BookCopyData();
        }

        public override void BuildConcreteData()
        {
            model.BookId = viewModel.BookId;
        }
    }
}