using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Relation.Interfaces
{
    public interface IRelationUserRoleRepository : IRepository<RelationUserRoleData>
    {
        RelationUserRoleData GetRelationUserRoleFor(int userId, int roleId);

        List<RelationUserRoleData> GetRelationUserRoleFor(int userId);
    }
}
