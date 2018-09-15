using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject.Relation
{
    public class RelationUserPermissionData : BaseData
    {
        [DBColumn("RefUser")]
        public int RefUser { get; set; }

        [DBColumn("RefPermission")]
        public int RefPermission { get; set; }
    }
}
