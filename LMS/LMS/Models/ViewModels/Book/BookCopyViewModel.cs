using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Book
{
    public class BookCopyViewModel : ViewModel
    {
        public int BookId { get; set; }

        public string BookAuthorAndTitle { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}