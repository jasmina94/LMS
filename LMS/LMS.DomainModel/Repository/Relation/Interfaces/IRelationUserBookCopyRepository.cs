using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Relation.Interfaces
{
    public interface IRelationUserBookCopyRepository : IRepository<RelationUserBookCopyData>
    {
        List<RelationUserBookCopyData> GetLoansForUser(int userId);
    }
}
