using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Entities.Gallery {

	[Table("Item")]
	public class Item : IActiveStatus {

		[Key]
		[Column("Item_Id")]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Column("Item_Gallery_Category_Id")]
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int GalleryCategoryId { get; set; }

		[Column("Item_Title")]
		[Required]
		public String Title { get; set; }

		[Column("Item_Description")]
		public Byte[] Description { get; set; }

		[DisplayName("Showcase Description")]
		[MaxLength(255)]
		[Column("Item_Showcase_Description")]
		public String ShowcaseDescription { get; set; }

		[NotMapped]
		public ActiveStatus ActiveStatus { get; set; }

		[ForeignKey("GalleryCategoryId")]
		public GalleryCategory GalleryCategory { get; set; }

		public ICollection<ItemImage> ItemImages { get; set; }

		public Item() {

		}

		[Column("Item_Active_Status")]
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

		[AllowHtml]
		[NotMapped]
		[Display(Name = "Description", Description = "Item Description")]
		public string DescriptionUTF8 {
			get {
				if (Description == null)
					return null;
				else
					return Encoding.UTF8.GetString(Description);
			}
			set {
				Description = Encoding.Default.GetBytes(value);
			}
		}
	}

}