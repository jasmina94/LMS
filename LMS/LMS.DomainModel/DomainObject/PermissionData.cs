using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class PermissionData : BaseData
    {
        [DBColumn("NamePermission")]
        public string NamePermission { get; set; }

        [DBColumn("CodePermission")]
        public string CodePermission { get; set; }
    }
}
