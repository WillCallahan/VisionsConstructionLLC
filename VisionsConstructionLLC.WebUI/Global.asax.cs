using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VisionsConstructionLLC.Database.Concrete;
using VisionsConstructionLLC.WebUI.App_Start;
using VisionsConstructionLLC.WebUI.Infrastructure;

namespace VisionsConstructionLLC.WebUI {
	public class MvcApplication : System.Web.HttpApplication {
		private ILog log = LogManager.GetLogger(typeof (MvcApplication));

		void Application_Error(Object sender, EventArgs e) {
			log.Error("Error: ", Server.GetLastError().GetBaseException());
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			XmlConfigurator.Configure();
			System.Data.Entity.Database.SetInitializer<EntityManagerDbContext>(null); //Disable Table Initialization
			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
		}
	}
}
