using System;
using System.Reflection;

namespace LMS.Infrastructure.Validation
{
    public interface ILMSValidator
    {
        string InvokeValidation<T>(T model) where T : class;

        MethodInfo FindSpecificMethod(string type, Type attributeType);
    }
}
