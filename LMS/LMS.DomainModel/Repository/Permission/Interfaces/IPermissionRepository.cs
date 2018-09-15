using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;

namespace LMS.DomainModel.Repository.Permission.Interfaces
{
    public interface IPermissionRepository : IRepository<PermissionData>
    {
        PermissionData GetPermissionByCode(string code);
    }
}
