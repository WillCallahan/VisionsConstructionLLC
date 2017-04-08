using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Entities.Gallery {

	[Table("Gallery_Category")]
	public class GalleryCategory : IActiveStatus {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Gallery_Category_Id")]
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[Column("Gallery_Category_Name")]
		[StringLength(50, MinimumLength = 1)]
		public String Name { get; set; }

		[NotMapped]
		public ActiveStatus ActiveStatus { get; set; }
		
		public ICollection<Item> Items { get; set; }

		public GalleryCategory() {

		}

		[Column("Gallery_Category_Active_Status")]
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

	}

}