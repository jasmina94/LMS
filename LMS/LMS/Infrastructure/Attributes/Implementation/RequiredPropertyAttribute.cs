using LMS.Infrastructure.Attributes.Abstraction;
using System;

namespace LMS.Infrastructure.Attributes.Implementation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public class RequiredPropertyAttribute : BasePropertyAttribute, IPropertyAttribute
    {
        public RequiredPropertyAttribute() : base()
        {
            typeOfAttribute = AttributeType.Required;
        }

        public RequiredPropertyAttribute(string message) : base()
        {
            validationMessage = message;
            typeOfAttribute = AttributeType.Required;
        }
    }
}