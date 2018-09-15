using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;

namespace LMS.DomainModel.Repository.Category.Implementation
{
    public class CategoryRepository : Repository<CategoryData>, ICategoryRepository
    {
        public CategoryRepository() : base()
        {

        }
    }
}
