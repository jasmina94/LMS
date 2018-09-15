﻿using LMS.DomainModel.Infrastructure.ORM.Attributes;

namespace LMS.DomainModel.DomainObject
{
    public class BookCopyData : BaseData
    {
        [DBColumn("RefBook")]
        public int RefBook { get; set; }
    }
}
