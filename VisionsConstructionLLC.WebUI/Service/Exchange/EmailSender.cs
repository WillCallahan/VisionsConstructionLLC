using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using log4net;
using System.Net;
using System.Text;

namespace VisionsConstructionLLC.WebUI.Service.Exchange {

	/// <summary>
	/// Email service to send emails to various recipients
	/// </summary>
	public class EmailSender : IEmailSender {
		private ILog log;
		private SmtpClient smtpClient;

		public EmailSender() {
			log = LogManager.GetLogger(this.GetType());
			smtpClient = new SmtpClient();
		}

		public void sendEmail(List<String> recipients, String subject, String body) {
			log.Info("Sending an email...");
			log.Debug("Email Information:"
				+ "\r\n:Recipients: " + recipients.ToString()
				+ "\r\nSubject: " + subject
				+ "\r\nBody: " + body);
			MailMessage mailMessage = new MailMessage();
			foreach (String recipient in recipients)
				mailMessage.To.Add(recipient);
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = false;
			mailMessage.BodyEncoding = Encoding.UTF8;
			smtpClient.Send(mailMessage);
			log.Info("Email Sent!");
		}
	}
}