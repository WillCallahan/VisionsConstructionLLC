using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using VisionsConstructionLLC.Database.Repository;
using VisionsConstructionLLC.Database.Repository.Gallery;
using VisionsConstructionLLC.WebUI.Service.Exchange;
using log4net;

namespace VisionsConstructionLLC.WebUI.Infrastructure {

	/// <summary>
	/// Configuration for the Ninject Controller IOC Container
	/// </summary>
	public class NinjectControllerFactory : DefaultControllerFactory {
		private ILog log;
		private IKernel iKernel;
		private IEmailSender iEmailSender;
		private IEmailHelper iEmailHelper;
		private IUserRepository iUserRepository;

		public NinjectControllerFactory() {
			log = LogManager.GetLogger(this.GetType());
			iKernel = new StandardKernel();
			iEmailSender = new EmailSender();
			iUserRepository = new UserRepository();
			initializeIEmailHelper();
			AddBindings();
		}

		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType) {
			return controllerType == null ? null : (IController)iKernel.Get(controllerType);
		}

		/// <summary>
		/// Adds the required bindings to the Ninject IOC Container
		/// </summary>
		private void AddBindings() {
			log.Debug("Attempting to setup Ninject Injections...");
			iKernel.Bind<IItemRepository>().To<ItemRepository>();
			iKernel.Bind<IGalleryCategoryRepository>().To<GalleryCategoryRepository>();
			iKernel.Bind<IItemImageRepository>().To<ItemImageRepository>();
			iKernel.Bind<IEmailRepository>().To<EmailRepository>();
			iKernel.Bind<IEmailSender>().ToConstant(iEmailSender);
			iKernel.Bind<IEmailHelper>().ToConstant(iEmailHelper);
			iKernel.Bind<IUserRepository>().ToConstant(iUserRepository);
			log.Debug("Ninject Injection Setup Complete!");
		}

		/// <summary>
		/// Initialized the <see cref="IEmailHelper"/> to be able
		/// to access email address from properties files.
		/// </summary>
		private void initializeIEmailHelper() {
			log.Info("Attempting to set email settings: " + WebConfigurationManager.AppSettings["EmailSender:Recipients"]);
			iEmailHelper = new EmailHelper(WebConfigurationManager.AppSettings["EmailSender:Recipients"]);
		}
	}
}