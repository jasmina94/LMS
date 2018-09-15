using LMS.DomainModel.Infrastructure.ORM.Attributes;
using System;

namespace LMS.DomainModel.DomainObject
{
    public class BaseData
    {
        [DBColumn("Id")]
        public int Id { get; set; }

        [DBColumn("IsActive")]
        public bool IsActive { get; set; }

        [DBColumn("RefUserCreatedBy")]
        public int RefUserCreatedBy { get; set; }

        [DBColumn("DateTimeCreatedOn")]
        public DateTime DateTimeCreatedOn { get; set; }

        [DBColumn("RefUserDeletedBy")]
        public int RefUserDeletedBy { get; set; }

        [DBColumn("DateTimeDeletedOn")]
        public DateTime DateTimeDeletedOn { get; set; }
    }
}
