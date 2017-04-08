using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.WebUI.Attribute;

namespace VisionsConstructionLLC.WebUI.Controllers {

	/// <summary>
	/// Constroller for the About Views
	/// </summary>
	[LocationLogger]
	[RoutePrefix("About")]
	public class AboutController : Controller {
		private ILog log;

		public AboutController() {
			log = LogManager.GetLogger(this.GetType());
		}

		/// <summary>
		/// Gets the About View
		/// </summary>
		/// <returns>About View</returns>
		[HttpGet]
		[Route]
		[Route("About")]
		public ActionResult About() {
			return View();
		}
	}
}