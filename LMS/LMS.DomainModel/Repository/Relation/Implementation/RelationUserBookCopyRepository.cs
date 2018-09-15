using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;

namespace LMS.DomainModel.Repository.Relation.Implementation
{
    public class RelationUserBookCopyRepository : Repository<RelationUserBookCopyData>, IRelationUserBookCopyRepository
    {
        public RelationUserBookCopyRepository() : base()
        {

        }
    }
}
