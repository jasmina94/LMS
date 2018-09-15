using Autofac;
using Autofac.Integration.Mvc;
using LMS.DomainModel.DataSource.Server;
using LMS.DomainModel.DataSource.Source;
using LMS.DomainModel.DataSource.Transaction;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Implementation;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Interfaces;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System;
using LMS.Infrastructure.Validation;
using LMS.Infrastructure.ModelConstructor.Implementation;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;

namespace LMS.App_Start
{
    public class AutofacConfig
    {
        private static ContainerBuilder builder = new ContainerBuilder();

        public static void RegisterDependencies()
        {
            RegisterControllers();
            RegisterDataSource();
            RegisterORMMapper();
            RegisterRepositories();
            RegisterValidator();
            RegisterModelConstruction();
            RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterControllers()
        {
            builder
            .RegisterControllers(typeof(MvcApplication).Assembly)
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterDataSource()
        {
            builder
            .RegisterType<SqlServerDatabase>()
            .As<ISqlServerDatabase>()
            .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["LMS"].ConnectionString)
            .SingleInstance();

            builder
               .Register(c => new LMSDataSource { SqlServerDatabase = c.Resolve<ISqlServerDatabase>() })
               .As<IDataSource>()
               .SingleInstance();

            builder
               .RegisterType<TransactionManager>()
               .As<ITransactionManager>()
               .SingleInstance();
        }

        private static void RegisterORMMapper()
        {
            builder
            .RegisterType<LMSMapper>()
            .As<ILMSMapper>()
            .SingleInstance();
        }

        private static void RegisterRepositories()
        {
            var dataAccess = Assembly.Load(new AssemblyName("LMS.DomainModel"));

            builder
               .RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.None);
        }

        private static void RegisterValidator()
        {
            builder
            .RegisterType<LMSValidator>()
            .As<ILMSValidator>()
            .SingleInstance()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterModelConstruction()
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder
               .RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("ModelBuilder"))
               .InstancePerDependency()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder
               .RegisterType<ModelConstructor>()
               .As<IModelConstructor>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder
               .RegisterType<BuilderResolverServiceImpl>()
               .As<IBuilderResolverService>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterFilterProvider()
        {
            builder.RegisterFilterProvider();
        }
    }
}