using Raven.Client.Document;
using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ToothCrystal.App_Start;

namespace ToothCrystal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Container Container { get; private set; }
        public static DocumentStore MyDocumentStore;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnitOfWorkConfig.Configure();
        }

    }
}
