using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject.Relation
{
    public class RelationRolePermissionData : BaseData
    {
        [DBColumn("RefRole")]
        public int RoleId { get; set; }

        [DBColumn("RefPermission")]
        public int PermissionId { get; set; }
    }
}
