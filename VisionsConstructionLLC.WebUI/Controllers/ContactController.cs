using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Entities;
using VisionsConstructionLLC.Database.Models.Enums;
using VisionsConstructionLLC.Database.Repository;
using VisionsConstructionLLC.Database.Repository.Gallery;
using VisionsConstructionLLC.WebUI.Attribute;
using VisionsConstructionLLC.WebUI.Service.Exchange;

namespace VisionsConstructionLLC.WebUI.Controllers {

	[LocationLogger]
	[RoutePrefix("Contact")]
	public class ContactController : Controller {
		private ILog log;
		[Inject]
		public IEmailRepository EmailRepository { private get; set; }
		[Inject]
		public IEmailSender EmailSender { private get; set; }
		[Inject]
		public IEmailHelper EmailHelper { private get; set; }

		public ContactController() {
			log = LogManager.GetLogger(this.GetType());
		}

		/// <summary>
		/// Used to recieve an email address as a string in order to
		/// fill the email address field of the Contact page.
		/// </summary>
		/// <param name="email">Email Address</param>
		/// <returns></returns>
		[HttpPost]
		[Route("SendEmail")]
		public ActionResult SendEmail(String email) {
			return View("Contact", new Email() { Address = email });
		}

		/// <summary>
		/// Sends an email to the recipient and to the sending
		/// email address.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SendMessage")]
		public ActionResult Contact(Email email) {
			log.Info("Attempting to send an email...");
			ViewBag.Success = true;
			try {
				email.ActiveStatus = ActiveStatus.ACTIVE;
				EmailRepository.create(email);
				EmailSender.sendEmail(EmailHelper.getAddresses(), "Request - " + email.Address, email.ToString());
				EmailSender.sendEmail(new List<String> { email.Address }, "DO NOT REPLY - Request Received - " + email.Address, email.ToString());
				ModelState.Clear();
			} catch (Exception e) {
				log.Error("An error occured while sending an email!", e);
				ViewBag.Message = e.Message;
				ViewBag.Success = false;
				return View(email);
			}
			return View();
		}

		/// <summary>
		/// Gets the Contact View
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route]
		[Route("Contact")]
		public ActionResult Contact() {
			return View();
		}
	}
}