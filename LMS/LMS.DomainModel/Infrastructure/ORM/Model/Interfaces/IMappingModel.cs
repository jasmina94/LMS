using System.Data.SqlClient;

namespace LMS.DomainModel.Infrastructure.ORM.Model.Interfaces
{
    public interface IMappingModel
    {
        SqlCommand ExecuteCommand(string commandType);
    }
}
