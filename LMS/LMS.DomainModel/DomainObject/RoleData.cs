using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class RoleData : BaseData
    {
        [DBColumn("NameRole")]
        public string NameRole { get; set; }

        [DBColumn("CodeRole")]
        public string CodeRole { get; set; }
    }
}
