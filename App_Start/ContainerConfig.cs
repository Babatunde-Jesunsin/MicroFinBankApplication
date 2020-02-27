using Autofac;
using Autofac.Integration.Mvc;
using BankData.DI;
using BankData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroFinBank.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Function>().As<IFunction>().InstancePerRequest();
            builder.RegisterType<MicroFinDbEntities>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}