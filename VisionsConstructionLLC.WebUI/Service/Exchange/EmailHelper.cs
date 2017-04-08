using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace VisionsConstructionLLC.WebUI.Service.Exchange {

	/// <summary>
	/// Email Helper class which store email addresses.
	/// This is particularly useful in storing information
	/// from configuration or properties files for injection.
	/// </summary>
	public class EmailHelper : IEmailHelper {
		private ILog log;
		private List<String> addresses;

		public EmailHelper() {
			log = LogManager.GetLogger(this.GetType());
			addresses = new List<String>();
		}

		public EmailHelper(String addresses) : this() {
			this.addresses.AddRange(extractEmails(addresses));
		}

		public EmailHelper(List<String> addresses) : this() {
			this.addresses.AddRange(addresses);
		}

		/// <summary>
		/// Gets the Email Address List.
		/// 
		/// NOTE: Pass-By_Value reference only requires this method.
		/// </summary>
		/// <returns><see cref="System.Collections.Generic.List"/> Addresses</returns>
		public List<String> getAddresses() {
			return addresses;
		}

		private List<String> extractEmails(String emails) {
			log.Info("Attempting to extract all email addresses from " + emails);
			List<String> addresses = emails.Split(',').ToList<String>();
			String pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
			foreach (String address in addresses) {
				if (!Regex.Match(address, pattern).Success)
					throw new ArgumentException("Email address (" + address + @") had an invalid format; must match ^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
			}
			return addresses;
		}
	}
}