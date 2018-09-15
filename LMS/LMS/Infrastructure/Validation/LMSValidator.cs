using LMS.Infrastructure.Attributes.Abstraction;
using LMS.Infrastructure.Validation.Model.Implementation;
using LMS.Infrastructure.Validation.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LMS.Infrastructure.Validation
{
    public class LMSValidator : ILMSValidator
    {
        private Dictionary<string, List<MethodInfo>> methodsForValidation { get; set; }
        private List<PropertyModel> propertyModels { get; set; }
        private Dictionary<Type, List<IValidationModel>> validationCollection { get; set; }

        public LMSValidator()
        {
            methodsForValidation = new Dictionary<string, List<MethodInfo>>();
            propertyModels = new List<PropertyModel>();
            validationCollection = new Dictionary<Type, List<IValidationModel>>();

            FindMethodsForValidation();
            FindPropertiesToValidate();
            GenerateValidationCollection();
        }

        private void FindMethodsForValidation()
        {
            Func<MethodInfo, string> keyQuery = k => PrepareKeyForMethodValidationDictionary(k.DeclaringType.Name);

            methodsForValidation = Assembly.GetExecutingAssembly().GetTypes()
               .SelectMany(t => t.GetMethods())
               .Where(m => m.GetCustomAttributes(typeof(BasePropertyAttribute), false).Length > 0)
               .GroupBy(keyQuery)
               .ToDictionary(m => m.Key, m => m.ToList());
        }

        private string PrepareKeyForMethodValidationDictionary(string name)
        {
            string key = "";
            int index = name.LastIndexOf("Validator");
            if (index != 0)
            {
                key = name.Remove(index).ToLower();
            }

            if (key.Equals("integer"))
            {
                key = "int32";
            }

            return key;
        }

        private void FindPropertiesToValidate()
        {
            Func<Type, bool> query = t => t.IsClass
            && t.Name.Contains("ViewModel") && !t.Name.Equals("ViewModel")
            && t.Namespace.Contains("LMS.MVC.Models.ViewModels");

            List<Type> models = Assembly.GetExecutingAssembly().GetTypes().Where(query).ToList();

            foreach (PropertyInfo propertyInfo in models.SelectMany(m => m.GetProperties()))
            {
                var attributes = propertyInfo.GetCustomAttributes(false).Where(a => a is IPropertyAttribute).Count();
                if (attributes != 0)
                {
                    GeneratePropertyModel(propertyInfo);
                }
            }
        }

        private void GeneratePropertyModel(PropertyInfo propertyInfo)
        {
            var propertyModel = new PropertyModel();
            propertyModel.ReferencedModelType = propertyInfo.DeclaringType;
            propertyModel.PropertyName = propertyInfo.Name;
            propertyModel.PropertyType = propertyInfo.PropertyType.Name;
            propertyModel.Attributes = propertyInfo.GetCustomAttributes(false);
            propertyModel.Property = propertyInfo;

            propertyModels.Add(propertyModel);
        }

        private void GenerateValidationCollection()
        {
            foreach (PropertyModel propertyModel in propertyModels)
            {
                Type typeOfModelToValidate = propertyModel.ReferencedModelType;
                if (!validationCollection.ContainsKey(typeOfModelToValidate))
                {
                    validationCollection[typeOfModelToValidate] = new List<IValidationModel>();
                }
                BindValidation(propertyModel);
            }
        }

        private void BindValidation(PropertyModel propertyModel)
        {
            Type typeOfModelToValidate = propertyModel.ReferencedModelType;
            foreach (object attribute in propertyModel.Attributes)
            {
                IPropertyAttribute baseAttribute = attribute as BasePropertyAttribute;
                if (baseAttribute != null)
                {
                    IValidationModel validationModel = MakeValidationModel(propertyModel, baseAttribute);
                    validationCollection[typeOfModelToValidate].Add(validationModel);
                }
            }
        }

        private IValidationModel MakeValidationModel(PropertyModel propertyModel, IPropertyAttribute attribute)
        {
            Type type = typeof(ValidationModel<>).MakeGenericType(propertyModel.ReferencedModelType);
            MethodInfo methodForValidation = FindSpecificMethod(propertyModel.PropertyType, attribute.GetType());
            object[] param = new[] { (object)propertyModel, (object)attribute, (object)methodForValidation };

            object instance = Activator.CreateInstance(type, param);

            return (IValidationModel)instance;
        }

        public MethodInfo FindSpecificMethod(string type, Type attributeType)
        {
            MethodInfo specificMethod = null;
            type = type.ToLower();
            if (methodsForValidation[type].Count > 0)
            {
                foreach (MethodInfo method in methodsForValidation[type])
                {
                    IPropertyAttribute attribute = (IPropertyAttribute)method.GetCustomAttribute(attributeType);
                    if (attribute != null)
                    {
                        specificMethod = method;
                        break;
                    }
                }
            }

            return specificMethod;
        }

        public string InvokeValidation<T>(T model) where T : class
        {
            string message = "";
            Type typeOfModel = model.GetType();
            List<IValidationModel> validationModelsForType = new List<IValidationModel>();

            if (validationCollection.TryGetValue(typeOfModel, out validationModelsForType))
            {
                if (validationModelsForType.Count > 0)
                {
                    foreach (var item in validationModelsForType)
                    {
                        var validationModel = (ValidationModel<T>)item;
                        validationModel.Model = model;

                        if (!validationModel.Execute())
                        {
                            message = validationModel.GetMessage();
                            break;
                        }
                    }
                }
            }

            return message;
        }
    }
}