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

        [DBColumn("Filename")]
        public string Filename { get; set; }

        [DBColumn("MIME")]
        public string MIME { get; set; }

        [DBColumn("RefCategory")]
        public int RefCategory { get; set; }

        [DBColumn("RefLanguage")]
        public int RefLanguage { get; set; }

        [DBColumn("RefCataloguer")]
        public int RefCataloguer { get; set; }
    }
}
