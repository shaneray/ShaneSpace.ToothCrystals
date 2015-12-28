using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace ToothCrystal.App_Start
{
    public class UnitOfWorkConfig
    {
        public static void Configure()
        {
            DependencyResolver.SetResolver(type => type == typeof(IAsyncActionInvoker) ? new UnitOfWorkAsyncActionInvoker() : null, type => Enumerable.Empty<object>());
        }
    }

    public class UnitOfWorkAsyncActionInvoker : AsyncControllerActionInvoker
    {
        protected override IAsyncResult BeginInvokeActionMethod(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor,IDictionary<string, object> parameters, AsyncCallback callback,object state)
        {
            return base.BeginInvokeActionMethod(controllerContext, actionDescriptor, parameters,
                                               result => DoSomethingAsyncAfterTask().ContinueWith(task => callback(task)),
                                               state);
        }

        public async Task DoSomethingAsyncAfterTask()
        {
            await Task.Delay(1000);
        }
    }
}