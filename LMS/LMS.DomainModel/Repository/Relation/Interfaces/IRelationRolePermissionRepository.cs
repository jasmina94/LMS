using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Relation.Interfaces
{
    public interface IRelationRolePermissionRepository : IRepository<RelationRolePermissionData>
    {
        List<RelationRolePermissionData> GetRelationRolePermissionFor(int roleId);
    }
}
