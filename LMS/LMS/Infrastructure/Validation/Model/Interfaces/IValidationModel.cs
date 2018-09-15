using LMS.Infrastructure.Attributes.Abstraction;
using LMS.Infrastructure.Validation.Model.Implementation;
using System.Reflection;

namespace LMS.Infrastructure.Validation.Model.Interfaces
{
    public interface IValidationModel
    {
        bool Execute();

        string GetMessage();

        void SetMessage(string message);

        void SetFunction(PropertyModel propertyModel, IPropertyAttribute attribute, MethodInfo validationMethod);
    }
}
