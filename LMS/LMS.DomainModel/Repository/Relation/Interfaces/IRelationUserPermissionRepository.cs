using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Relation.Interfaces
{
    public interface IRelationUserPermissionRepository : IRepository<RelationUserPermissionData>
    {
        List<RelationUserPermissionData> GetRelationUserPermissionFor(int userId);
    }
}
