using LMS.DomainModel.Infrastructure.ORM.Attributes;
using System;

namespace LMS.DomainModel.DomainObject.Relation
{
    public class RelationUserBookCopyData : BaseData
    {
        [DBColumn("DateOfIssue")]
        public DateTime DateOfIssue { get; set; }

        [DBColumn("DateDueForReturn")]
        public DateTime DateDueForReturn { get; set; }

        [DBColumn("DateReturned")]
        public DateTime DateReturned { get; set; }

        [DBColumn("RefBookCopy")]
        public int BookCopyId { get; set; }

        [DBColumn("RefUser")]
        public int UserId { get; set; }
    }
}
