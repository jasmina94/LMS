using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject.Relation
{
    public class RelationUserRoleData : BaseData
    {
        [DBColumn("RefUser")]
        public int UserId { get; set; }

        [DBColumn("RefRole")]
        public int RoleId { get; set; }
    }
}
