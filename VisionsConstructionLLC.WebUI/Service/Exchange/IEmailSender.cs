using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionsConstructionLLC.WebUI.Service.Exchange {
	public interface IEmailSender {

		/// <summary>
		/// Sends an email to a list of recipients
		/// </summary>
		/// <param name="recipients">Recipients to Send email to</param>
		/// <param name="subject">Subject of the email</param>
		/// <param name="body">Body of the email</param>
		void sendEmail(List<String> recipients, String subject, String body);

	}
}
