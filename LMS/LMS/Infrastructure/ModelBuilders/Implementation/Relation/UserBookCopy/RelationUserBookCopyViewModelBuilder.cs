using System;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Relation;
using LMS.DomainModel.DomainObject;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserBookCopy
{
    public class RelationUserBookCopyViewModelBuilder : ViewModelBuilder<RelationUserBookCopyViewModel, RelationUserBookCopyData>
    {
        public IBookRepository BookRepository { get; set; }

        public IBookCopyRepository BookCopyRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public RelationUserBookCopyViewModelBuilder(RelationUserBookCopyData model) : base(model)
        {
            viewModel = new RelationUserBookCopyViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.BookCopyId = model.BookCopyId;
            viewModel.BookFullName = GetBookFullTitle(model.BookCopyId);
            viewModel.UserId = model.UserId;
            viewModel.UserFullName = GetUserFullName(model.UserId);
            viewModel.DateOfIssue = model.DateOfIssue;
            viewModel.DateDueForReturn = model.DateDueForReturn;
            viewModel.DateReturned = model.DateReturned;
        }

        private string GetUserFullName(int userId)
        {
            string fullname = string.Empty;
            UserData user = UserRepository.GetDataById(userId);
            if (user != null)
            {
                fullname = user.FullFirstAndLastName;
            }

            return fullname;
        }

        private string GetBookFullTitle(int bookCopyId)
        {
            string fulltitle = string.Empty;
            BookCopyData bookCopy = BookCopyRepository.GetDataById(bookCopyId);
            if (bookCopy != null)
            {
                BookData book = BookRepository.GetDataById(bookCopy.BookId);
                if(book != null)
                {
                    fulltitle = book.BookAuthorAndTitle;
                }
            }

            return fulltitle;
        }
    }
}