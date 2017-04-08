using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.WebUI.Attribute;

namespace VisionsConstructionLLC.WebUI.Controllers {

	[LocationLogger]
	public class HomeController : Controller {
		private ILog log;

		public HomeController() {
			log = LogManager.GetLogger(this.GetType());
		}

		[Route]
		[Route("Home")]
		public ActionResult Index() {
			return View();
		}
	}
}