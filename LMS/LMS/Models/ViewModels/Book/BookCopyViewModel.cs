using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Book
{
    public class BookCopyViewModel : ViewModel
    {
        [RequiredProperty("Book copy must have referencing book!")]
        public int BookId { get; set; }

        [RequiredProperty("Book copy must have referencing book!")]
        public string BookAuthorAndTitle { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}