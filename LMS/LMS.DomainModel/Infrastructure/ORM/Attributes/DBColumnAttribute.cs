using System;

namespace LMS.DomainModel.Infrastructure.ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DBColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public DBColumnAttribute()
        {
        }

        public DBColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
