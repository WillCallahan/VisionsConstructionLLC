using log4net;
using Ninject;
using System;
using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;
using System.Web.Security;
using VisionsConstructionLLC.Database.Repository;
using VisionsConstructionLLC.WebUI.Attribute;
using VisionsConstructionLLC.WebUI.Model;

namespace VisionsConstructionLLC.WebUI.Controllers.Authentication {

	[LocationLogger]
	[RoutePrefix("Authentication")]
	public class AuthenticationController : Controller {
		private ILog log;
		public string Domain { get; set; }
		public ContextType ContextType { get; set; }
		[Inject]
		public IUserRepository UserRepository { private get; set; }

		public AuthenticationController() {
			ContextType = ContextType.Machine;
			Domain = null;
			log = LogManager.GetLogger(this.GetType());
		}

		[HttpGet]
		[Route("Login")]
		[ActionName("Login")]
		public ActionResult Index() {
			return View();
		}

		/// <summary>
		/// Attempst to authenticate a user on the local machine
		/// </summary>
		/// <param name="login">Login Object containg credentials</param>
		/// <returns><see cref="ActionResult"/> Index View</returns>
		[HttpPost]
		[Route("Login")]
		public ActionResult Login(Login login) {
			log.Info("Attempting to login user with a username of " + login.Username);
			if (!this.ModelState.IsValid)
				return View(login);
			try {
				UserRepository.find(login.Username, login.Password);
				log.Info("User has been authenticated with a username of " + login.Username);
				FormsAuthentication.SetAuthCookie(login.Username, false);
				return RedirectToAction("GalleryAdministration", "GalleryAdministration");
			} catch (InvalidOperationException e) {
				log.Fatal("Failed Authentication Attemp!", e);
			}
			log.Warn("Unable to login user with a username of " + login.Username);
			ModelState.Clear();
			ModelState.AddModelError(string.Empty, "Username of password is incorrect. Please try again.");
			return View();
		}

		/// <summary>
		/// Logs out a user by invalidating the session
		/// </summary>
		/// <returns><see cref="ActionResult"/> Index View</returns>
		[HttpGet]
		[ActionName("Logout")]
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}