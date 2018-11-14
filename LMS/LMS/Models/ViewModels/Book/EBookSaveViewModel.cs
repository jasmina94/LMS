namespace LMS.Models.ViewModels.Book
{
    public class EBookCreateViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Keywords { get; set; }

        public int LanguageId { get; set; }

        public int CategoryId { get; set; }

        public int PublicationYear { get; set; }

        public string Filename { get; set; }
    }
}