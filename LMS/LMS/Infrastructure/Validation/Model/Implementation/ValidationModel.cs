using LMS.Infrastructure.Attributes;
using LMS.Infrastructure.Attributes.Abstraction;
using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation.Model.Interfaces;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace LMS.Infrastructure.Validation.Model.Implementation
{
    public class ValidationModel<T> : IValidationModel where T : class
    {
        private string message;
        private Func<T, bool> function;
        public T Model { get; set; }

        public ValidationModel(PropertyModel propertyModel, IPropertyAttribute attribute, MethodInfo methodForValidation)
        {
            string message = attribute.GetValidationMessage();
            SetMessage(message);
            SetFunction(propertyModel, attribute, methodForValidation);
        }

        public void SetMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SetFunction(PropertyModel propertyModel, IPropertyAttribute attribute, MethodInfo validationMethod)
        {
            Func<T, bool> predicate = null;
            MethodCallExpression validator = null;
            Type typeOfViewModel = propertyModel.ReferencedModelType;
            Type typeOfAttribute = attribute.GetType();

            ParameterExpression pe = Expression.Parameter(typeOfViewModel, "instance");
            Expression exp = Expression.Call(pe, propertyModel.Property.GetGetMethod());

            validator = GenerateMethodCallExpression(attribute, validationMethod, exp);
            predicate = Expression.Lambda<Func<T, bool>>(validator, pe).Compile();

            function = predicate;
        }

        private MethodCallExpression GenerateMethodCallExpression(IPropertyAttribute attribute, MethodInfo validationMethod, Expression expressionForValidation)
        {
            AttributeType type = attribute.GetAttributeType();
            MethodCallExpression exp = null;
            switch (type)
            {
                case AttributeType.Required:
                    exp = ValidationExpressionGenerator.GenerateRequiredExpression(validationMethod, expressionForValidation);
                    break;
                case AttributeType.RangeLength:
                    exp = ValidationExpressionGenerator.GenerateLengthRangeExpression((LengthPropertyAttribute)attribute, validationMethod, expressionForValidation);
                    break;
                default:
                    throw new InvalidEnumArgumentException("There is no expression for specified enum queryType!");
            }

            return exp;
        }

        public string GetMessage()
        {
            throw new NotImplementedException();
        }

        public bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}