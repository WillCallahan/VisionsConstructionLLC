using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Entities;
using VisionsConstructionLLC.Database.Repository;
using VisionsConstructionLLC.WebUI.Attribute;

namespace VisionsConstructionLLC.WebUI.Controllers.EmailAdministration {

	[Authorize]
	[RoutePrefix("EmailAdministration")]
	[LocationLogger]
	public class EmailAdministrationController : Controller {
		private ILog log;

		[Inject]
		public IEmailRepository EmailRepository { private get; set; }

		public EmailAdministrationController() {
			log = LogManager.GetLogger(this.GetType());
		}

		[HttpGet]
		[Route]
		[Route("Email")]
		public ActionResult Index() {
			return View(EmailRepository.findAll());
		}

		[HttpGet]
		[Route("EmailItem/{id}")]
		public ActionResult EmailItem(int id) {
			Email email = EmailRepository.find(id);
			if (email == null) {
				log.Warn("Unable to find email!");
				ModelState.AddModelError(String.Empty, "Unable to find email!");
				return RedirectToAction("Email");
			}
			return View(email);
		}
	}
}