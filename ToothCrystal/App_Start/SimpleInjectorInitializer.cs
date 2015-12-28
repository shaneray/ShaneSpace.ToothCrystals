using Microsoft.AspNet.Identity;
using Raven.Client;
using Raven.Client.Document;
using RavenDB.AspNet.Identity;
using ToothCrystal.Classes;
using ToothCrystal.Classes.Customer;
using ToothCrystal.Classes.FAQ;
using ToothCrystal.Classes.Inventory;
using ToothCrystal.Classes.ServiceRendered;
using ToothCrystal.Models;

[assembly: WebActivator.PostApplicationStartMethod(typeof(ToothCrystal.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace ToothCrystal.App_Start
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using System.Reflection;
    using System.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
            var container = new Container();

            container.EnableLifetimeScoping();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcAttributeFilterProvider();

            RegisterRaven(container);

            InitializeContainer(container);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            // For instance:
            container.RegisterSingle<IFaqManager, FaqManager>();
            container.RegisterSingle<ICustomerManager, CustomerManager>();
            container.RegisterSingle<IInventoryManager, InventoryManager>();
            container.RegisterSingle<IServiceRenderedManager, ServiceRenderedManager>();

            // register user manager
            container.RegisterPerWebRequest<UserManager<ApplicationUser>>(() => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DependencyResolver.Current.GetService<IDocumentSession>())));
        }

        private static void RegisterRaven(Container container)
        {
            IDocumentStore trackingDataStore = new DocumentStore
            {
                ConnectionStringName = "ToothCrystalDB"
            };
            trackingDataStore.Conventions.IdentityPartsSeparator = "-";
            trackingDataStore.Conventions.MaxNumberOfRequestsPerSession = 100;
            trackingDataStore.Initialize();

            container.RegisterSingle<IDocumentStore>(() => trackingDataStore);
            container.RegisterPerWebRequest<IAsyncDataDocumentSession>(() => new AsyncDataDocumentSession(trackingDataStore.OpenAsyncSession()));
            container.RegisterPerWebRequest<IDataDocumentSession>(() => new DataDocumentSession(trackingDataStore.OpenSession()));

            container.RegisterPerWebRequest<IDocumentSession>(() => new DataDocumentSession(trackingDataStore.OpenSession()));
        }

    }
}