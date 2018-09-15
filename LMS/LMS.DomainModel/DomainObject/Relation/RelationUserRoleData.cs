using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject.Relation
{
    public class RelationUserRoleData : BaseData
    {
        [DBColumn("RefUser")]
        public int RefUser { get; set; }

        [DBColumn("RefRole")]
        public int RefRole { get; set; }
    }
}
