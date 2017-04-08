using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Entities.Gallery {

	[Table("Item_Image")]
	public class ItemImage : IActiveStatus {

		[Key]
		[Column("Item_Image_Id")]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Column("Item_Image_Image")]
		public Byte[] Image { get; set; }

		[Column("Item_Image_Item_Id")]
		[Required]
		public int ItemId { get; set; }

		[Column("Item_Image_Meme_Type")]
		public string MemeType { get; set; }

		[NotMapped]
		public ActiveStatus ActiveStatus { get; set; }

		[ForeignKey("ItemId")]
		public Item Item { get; set; }

		public ItemImage() {

		}

		[Column("Item_Image_Active_Status")]
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