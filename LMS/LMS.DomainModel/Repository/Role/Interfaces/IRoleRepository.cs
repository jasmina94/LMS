using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;

namespace LMS.DomainModel.Repository.Role.Interfaces
{
    public interface IRoleRepository : IRepository<RoleData>
    {
        RoleData GetRoleByCode(string code);
    }
}
