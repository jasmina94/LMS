using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;

namespace LMS.DomainModel.Repository.User.Interfaces
{
    public interface IUserRepository : IRepository<UserData>
    {
        UserData GetUserByUsername(string username);

        UserData GetUserByEmail(string email);
    }
}
