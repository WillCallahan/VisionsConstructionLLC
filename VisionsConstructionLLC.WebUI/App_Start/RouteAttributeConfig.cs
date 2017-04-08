using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace VisionsConstructionLLC.WebUI.App_Start {
	public class RouteAttributeConfig {

		public static void Register(HttpConfiguration httpConfiguration) {
			httpConfiguration.MapHttpAttributeRoutes();
		}

	}
}