using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserBookCopy
{
    public class RelationUserBookCopyDomainModelBuilder : DomainModelBuilder<RelationUserBookCopyData, RelationUserBookCopyViewModel>
    {
        public RelationUserBookCopyDomainModelBuilder(RelationUserBookCopyViewModel viewModel) : base(viewModel)
        {
            model = new RelationUserBookCopyData();
        }

        public override void BuildConcreteData()
        {
            model.UserId = viewModel.UserId;
            model.BookCopyId = viewModel.BookCopyId;
            model.DateOfIssue = viewModel.DateOfIssue;
            model.DateDueForReturn = viewModel.DateDueForReturn;
            model.DateReturned = viewModel.DateReturned;
        }
    }
}