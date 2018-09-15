using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Language.Interfaces;

namespace LMS.DomainModel.Repository.Language.Implementation
{
    public class LanguagesRepository : Repository<LanguageData>, ILanguageRepository
    {
        public LanguagesRepository() : base()
        {

        }
    }
}
