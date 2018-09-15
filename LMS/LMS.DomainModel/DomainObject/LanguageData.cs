using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class LanguageData : BaseData
    {
        [DBColumn("NameLanguage")]
        public string NameLanguage { get; set; }

        [DBColumn("CodeLanguage")]
        public string CodeLanguage { get; set; }
    }
}
