using System;

namespace LMS.DomainModel.Service
{
    public static class NamingService
    {
        public static string GetIdName(Type domainModel)
        {
            string idName = "";
            string dataName = domainModel.Name;
            int index = dataName.LastIndexOf("Data");
            idName = "Id" + dataName.Remove(index);

            return idName;
        }

        public static string GetTableNameFromDomainModel(Type domainModel)
        {
            string tableName = domainModel.Name;
            int index = tableName.LastIndexOf("Data");
            tableName = tableName.Remove(index);

            if (tableName.Equals("User") || tableName.Equals("Role") || tableName.Equals("Language"))
            {
                tableName += "s";
            }

            return tableName;
        }

        public static string GetTableNameFromRepository(Type repository)
        {
            string className = repository.Name;
            int index = className.LastIndexOf("Repository");
            string tableName = className.Remove(index);

            return tableName;
        }
    }
}
