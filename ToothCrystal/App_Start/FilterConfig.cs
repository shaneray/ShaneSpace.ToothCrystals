using Raven.Client;
using System;
using System.Web.Mvc;
using ToothCrystal.Classes;

namespace ToothCrystal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RavenSessionAttribute());
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class RavenSessionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                // don't commit changes if an exception was thrown
                return;
            }

            using (IAsyncDataDocumentSession asyncDocumentSession = DependencyResolver.Current.GetService<IAsyncDataDocumentSession>())
            {
                asyncDocumentSession.SaveChangesAsync().ContinueWith(x => { });
                asyncDocumentSession.Dispose();
            }
            using (IDataDocumentSession asyncDocumentSession = DependencyResolver.Current.GetService<IDataDocumentSession>())
            {
                asyncDocumentSession.SaveChanges();
                asyncDocumentSession.Dispose();

            }
            using (IDocumentSession documentSession = DependencyResolver.Current.GetService<IDocumentSession>())
            {
                documentSession.SaveChanges();
                documentSession.Dispose();
            }
        }
    }
}
