using Autofac;
using Autofac.Integration.Mvc;
using LMS.BusinessLogic.AccessControlManagement.Implementation;
using LMS.BusinessLogic.AccessControlManagement.Interfaces;
using LMS.DomainModel.DataSource.Server;
using LMS.DomainModel.DataSource.Source;
using LMS.DomainModel.DataSource.Transaction;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Implementation;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Interfaces;
using LMS.Infrastructure.Authorization.Abstraction;
using LMS.Infrastructure.Authorization.Implementation;
using LMS.Infrastructure.ModelConstructor.Implementation;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Infrastructure.Validation;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using LMS.Chat;

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
            RegisterMembershipProvider();
            RegisterAccessControlService();
            RegisterBusinessLogicLayer();
            RegisterChatHub();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static IContainer GetContainer()
        {
            return builder.Build();
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

        private static void RegisterMembershipProvider()
        {
            builder
               .RegisterType<LMSMembershipProvider>()
               .As<ILMSMembershipProvider>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterAccessControlService()
        {
            builder
               .RegisterType<AccessControlServiceImpl>()
               .As<IAccessControlService>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterBusinessLogicLayer()
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder
            .RegisterAssemblyTypes(dataAccess)
            .Where(t => t.Name.EndsWith("ServiceImpl") && t.Namespace.Contains("Management"))
            .AsImplementedInterfaces()
            .SingleInstance()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void RegisterChatHub()
        {
            builder.RegisterType<LMSChatHub>().ExternallyOwned();
        }
    }
}