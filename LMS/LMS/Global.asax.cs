using Autofac;
using Autofac.Integration.Mvc;
using LMS.BusinessLogic.AccessControlManagement.Implementation;
using LMS.BusinessLogic.AccessControlManagement.Interfaces;
using LMS.Chat;
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
using System.Web.Optimization;
using System.Web.Routing;

namespace LMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer Container { get; set; }

        private ContainerBuilder builder = new ContainerBuilder();

        protected void Application_Start()
        {
            Container = RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private IContainer RegisterAutofac()
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

            Container = builder.Build();

            //set the dependency resolver for MVC to be Autofac.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            return Container;
        }

        private void RegisterControllers()
        {
            builder
                .RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterDataSource()
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

        private void RegisterORMMapper()
        {
            builder
            .RegisterType<LMSMapper>()
            .As<ILMSMapper>()
            .SingleInstance();
        }

        private void RegisterRepositories()
        {
            var dataAccess = Assembly.Load(new AssemblyName("LMS.DomainModel"));

            builder
               .RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.None);
        }

        private void RegisterValidator()
        {
            builder
                .RegisterType<LMSValidator>()
                .As<ILMSValidator>()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterModelConstruction()
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

        private void RegisterFilterProvider()
        {
            builder.RegisterFilterProvider();
        }

        private void RegisterMembershipProvider()
        {
            builder
               .RegisterType<LMSMembershipProvider>()
               .As<ILMSMembershipProvider>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterAccessControlService()
        {
            builder
               .RegisterType<AccessControlServiceImpl>()
               .As<IAccessControlService>()
               .SingleInstance()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterBusinessLogicLayer()
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("ServiceImpl") && t.Namespace.Contains("Management"))
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterChatHub()
        {
            builder.RegisterType<LMSChatHub>().ExternallyOwned();
        }
    }
}
