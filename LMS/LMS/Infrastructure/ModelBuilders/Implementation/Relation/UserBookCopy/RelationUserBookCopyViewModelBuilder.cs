using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserBookCopy
{
    public class RelationUserBookCopyViewModelBuilder : ViewModelBuilder<RelationUserBookCopyViewModel, RelationUserBookCopyData>
    {
        public RelationUserBookCopyViewModelBuilder(RelationUserBookCopyData model) : base(model)
        {
            viewModel = new RelationUserBookCopyViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.BookCopyId = model.BookCopyId;
            viewModel.UserId = model.UserId;
            viewModel.DateOfIssue = model.DateOfIssue;
            viewModel.DateDueForReturn = model.DateDueForReturn;
            viewModel.DateReturned = model.DateReturned;
        }
    }
}