using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Entities {

	[Table("User")]
	public class User : IActiveStatus {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("User_Id")]
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Column("User_Username")]
		[Required]
		[StringLength(100, MinimumLength = 1)]
		public String Username { get; set; }

		[Column("User_Password")]
		[Required]
		[StringLength(100, MinimumLength = 1)]
		public String Password { get; set; }

		[NotMapped]
		public ActiveStatus ActiveStatus { get; set; }

		public User() {

		}

		public User(String username, String password) {
			Username = username;
			Password = password;
		}

		[Column("User_Active_Status")]
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

		public override string ToString() {
			return "User:"
				+ "\r\n\tId: " + Id
				+ "\r\n\tUsername: " + Username;
		}
	}
}
