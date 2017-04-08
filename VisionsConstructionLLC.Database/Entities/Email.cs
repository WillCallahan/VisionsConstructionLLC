using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Entities {

	[Table("Email")]
	public class Email : IActiveStatus {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Email_Id")]
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Column("Email_Name")]
		[Required]
		[StringLength(40, MinimumLength = 1)]
		public String Name { get; set; }

		[Required]
		[RegularExpression(@".+@.+\..+")]
		[Column("Email_Address")]
		public String Address { get; set; }

		[StringLength(30)]
		[Column("Email_Phone")]
		public String Phone { get; set; }

		[Required]
		[StringLength(500, MinimumLength = 1)]
		[Column("Email_Message")]
		public String Message { get; set; }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		[Column("Email_Date")]
		public DateTime Date { get; private set; }

		[NotMapped]
		public ActiveStatus ActiveStatus { get; set; }

		public Email() {
			Date = DateTime.Now;
		}

		public Email(String name, String address, String message) : this() {
			Name = name;
			Address = address;
			Message = message;
		}

		public Email(int id, String name, String address, String message) : this(name, address, message) {
			Id = id;
		}

		[Column("Email_Active_Status")]
		public int ActiveStatusCode {
			get {
				if (ActiveStatus == null)
					return ActiveStatus.ACTIVE.Code;
				else
					return ActiveStatus.Code;
			}
			set {
				ActiveStatus = ActiveStatus.Values.ElementAt(value);
			}
		}

		public override String ToString() {
			return "Email:"
				+ "\r\n\tId: " + Id
				+ "\r\n\tName: " + Name
				+ "\r\n\tEmail Address: " + Address
				+ "\r\n\tPhone: " + Phone
				+ "\r\n\tMessage: " + Message;
		}
	}

}