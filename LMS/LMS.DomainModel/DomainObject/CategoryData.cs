using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class CategoryData : BaseData
    {
        [DBColumn("NameCategory")]
        public string NameCategory { get; set; }

        [DBColumn("CodeCategory")]
        public string CodeCategory { get; set; }
    }
}
