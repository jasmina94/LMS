using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class BookData : BaseData
    {
        [DBColumn("IsElectronic")]
        public bool IsElectronic { get; set; }

        [DBColumn("NumOfAvailableCopies")]
        public int NumOfAvailableCopies { get; set; }

        [DBColumn("Title")]
        public string Title { get; set; }

        [DBColumn("Author")]
        public string Author { get; set; }

        [DBColumn("PublicationYear")]
        public int PublicationYear { get; set; }

        [DBColumn("Keywords")]
        public string Keywords { get; set; }

        [DBColumn("BookFilename")]
        public string Filename { get; set; }

        [DBColumn("MIME")]
        public string MIME { get; set; }

        [DBColumn("RefCategory")]
        public int CategoryId { get; set; }

        [DBColumn("RefLanguage")]
        public int LanguageId { get; set; }

        [DBColumn("RefCataloguer")]
        public int CataloguerId { get; set; }

        public string BookAuthorAndTitle
        {
            get
            {
                return string.Join("-", Author, Title);
            }
        }

        public string BookTitleAndAuthorAndYear
        {
            get
            {
                return Title + "-" + Author + "(" + PublicationYear + ")";
            }
        }
    }
}
