using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Book;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Book
{
    public class BookDomainModelBuilder : DomainModelBuilder<BookData, BookViewModel>
    {
        public BookDomainModelBuilder(BookViewModel viewModel) : base(viewModel)
        {
            model = new BookData();
        }

        public override void BuildConcreteData()
        {
            model.IsElectronic = viewModel.IsElectronic;
            model.NumOfAvailableCopies = viewModel.NumOfAvailableCopies;
            model.Title = viewModel.Title;
            model.Author = viewModel.Author;
            model.Keywords = viewModel.Keywords == null ? string.Empty : viewModel.Keywords;
            model.PublicationYear = viewModel.PublicationYear;
            model.Filename = viewModel.Filename == null ? string.Empty : viewModel.Filename;
            model.MIME = viewModel.MIME == null ? string.Empty : viewModel.MIME;
            model.CataloguerId = viewModel.CataloguerId;
            model.CategoryId = viewModel.CategoryId;
            model.LanguageId = viewModel.LanguageId;
        }
    }
}