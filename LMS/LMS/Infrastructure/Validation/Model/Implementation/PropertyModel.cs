using System;
using System.Reflection;

namespace LMS.Infrastructure.Validation.Model.Implementation
{
    public class PropertyModel
    {
        public PropertyInfo Property { get; set; }

        public Type ReferencedModelType { get; set; }

        public string PropertyName { get; set; }

        public string PropertyType { get; set; }

        public object[] Attributes { get; set; }
    }
}