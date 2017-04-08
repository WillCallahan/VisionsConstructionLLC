using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;
using VisionsConstructionLLC.Database.Repository.Gallery;

namespace VisionsConstructionLLC.WebUI.Controllers.Shared {

	[RoutePrefix("Shared/Menu")]
	public class MenuController : Controller {
		[Inject]
		public IGalleryCategoryRepository GalleryCategoryRepository { private get; set; }

		[Route("_Menu")]
		public ActionResult _Menu() {
			return PartialView(GalleryCategoryRepository.findAll(ActiveStatus.ACTIVE));
		}
	}
}