using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Book
{
    public class BookViewModel : ViewModel
    {
        public bool IsElectronic { get; set; }

        public string IsElectronicLabel { get; set; }

        [RequiredProperty("Book title is required!")]
        public string Title { get; set; }

        [RequiredProperty("Book author is required!")]
        public string Author { get; set; }

        public int PublicationYear { get; set; }

        [RequiredProperty("Number of available copies of book is required!")]
        public int NumOfAvailableCopies { get; set; }

        public string Filename { get; set; }

        public string MIME { get; set; }

        public string Keywords { get; set; }

        [RequiredProperty("Book category is required!")]
        public int CategoryId { get; set; }

        public string Category { get; set; }

        [RequiredProperty("Book language is required!")]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        public int CataloguerId { get; set; }

        public string Cataloguer { get; set; }

        public string AuthorAndTitle
        {
            get
            {
                return Author + " - " + Title;
            }
        }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}