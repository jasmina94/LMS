using System;
using System.Collections.Generic;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Interfaces;
using LMS.DomainModel.Infrastructure.ORM.Model.Interfaces;
using System.Reflection;
using System.Linq;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;

namespace LMS.DomainModel.Infrastructure.ORM.Mapper.Implementation
{
    public class LMSMapper : ILMSMapper
    {
        private Dictionary<Type, IMappingModel> mappingCollection;
        private Dictionary<Type, MethodInfo> fromDBMethodConversion;
        private Dictionary<Type, MethodInfo> toDBMethodConversion;

        public LMSMapper()
        {
            GenerateFromDBCollection();
            GenerateToDBCollection();
            GenerateMappingCollection();
        }

        private void GenerateFromDBCollection()
        {
            fromDBMethodConversion = new Dictionary<Type, MethodInfo>();

            Assembly dataAssembly = Assembly.Load(new AssemblyName("LMS.DomainModel"));

            fromDBMethodConversion = dataAssembly.GetTypes()
               .Where(t => t.Name.Equals("ConversionHelper"))
               .SelectMany(t => t.GetMethods())
               .Where(m => m.Name.EndsWith("FromDB") || m.Name.EndsWith("FromBigint"))
               .ToDictionary(m => m.ReturnType, m => m);
        }

        private void GenerateToDBCollection()
        {
            toDBMethodConversion = new Dictionary<Type, MethodInfo>();

            Assembly dataAssembly = Assembly.Load(new AssemblyName("LMS.DomainModel"));

            toDBMethodConversion = dataAssembly.GetTypes()
               .Where(t => t.Name.Equals("ConversionHelper"))
               .SelectMany(t => t.GetMethods())
               .Where(m => m.Name.StartsWith("ToDB"))
               .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }

        private void GenerateMappingCollection()
        {
            mappingCollection = new Dictionary<Type, IMappingModel>();

            Func<Type, bool> query = t => t.IsClass
               && t.Name.Contains("Data") && !t.Name.Equals("BaseData")
               && t.Namespace.Contains("LMS.DomainModel.DomainObject");

            Assembly dataAssembly = Assembly.Load(new AssemblyName("LMS.DomainModel"));
            List<Type> modelTypes = dataAssembly.GetTypes().Where(query).ToList();

            foreach (Type modelType in modelTypes)
            {
                if (!mappingCollection.ContainsKey(modelType))
                {
                    IMappingModel mappingModel = MakeMappingModel(modelType);
                    mappingCollection[modelType] = mappingModel;
                }
            }
        }

        private IMappingModel MakeMappingModel(Type modelType)
        {
            Type type = typeof(MappingModel<>).MakeGenericType(modelType);
            object[] param = new[] { fromDBMethodConversion, toDBMethodConversion };
            object instance = Activator.CreateInstance(type, param);

            return (IMappingModel)instance;
        }

        public IMappingModel GetMappingModelForType(Type modelType)
        {
            return mappingCollection[modelType];
        }

        public Dictionary<Type, IMappingModel> GetMappingCollection()
        {
            return mappingCollection;
        }
    }
}
