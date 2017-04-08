using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisionsConstructionLLC.WebUI.Attribute {

	/// <summary>
	/// Action Attribute to log information about when a controller action
	/// method is entered an exited.
	/// </summary>
	public class LocationLogger : ActionFilterAttribute {
		private ILog log;

		public LocationLogger() {

		}

		public LocationLogger(Type type) {
			log = LogManager.GetLogger(type.GetType());
		}

		/// <summary>
		/// Logs the PathAndQuery part of a URL before executing the method
		/// </summary>
		/// <param name="filterContext"><see cref="ActionExecutingContext"/></param>
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			base.OnActionExecuting(filterContext);
			if (log == null)
				log = LogManager.GetLogger(filterContext.Controller.GetType());
			log.Info("Entering " + filterContext.HttpContext.Request.Url.PathAndQuery);
		}

		/// <summary>
		/// Logs when a Controller Action is completed
		/// </summary>
		/// <param name="filterContext"><see cref="ActionExecutingContext"/></param>
		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			base.OnActionExecuted(filterContext);
			if (log == null)
				log = LogManager.GetLogger(filterContext.Controller.GetType());
			log.Debug("Exiting " + filterContext.HttpContext.Request.Url.PathAndQuery);
		}

	}
}