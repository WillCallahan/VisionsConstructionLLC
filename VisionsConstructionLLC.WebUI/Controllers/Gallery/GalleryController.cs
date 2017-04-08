using log4net;
using Ninject;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Entities.Gallery;
using VisionsConstructionLLC.Database.Models.Enums;
using VisionsConstructionLLC.Database.Repository.Gallery;
using VisionsConstructionLLC.WebUI.Attribute;

namespace VisionsConstructionLLC.WebUI.Controllers.Gallery {

	[LocationLogger]
	[RoutePrefix("Gallery")]
	public class GalleryController : Controller {
		private ILog log;

		[Inject]
		public IItemRepository ItemRepository { private get; set; }

		[Inject]
		public IItemImageRepository ItemImageRepository { private get; set; }

		[Inject]
		public IGalleryCategoryRepository GalleryCategoryRepository { private get; set; }

		public GalleryController() {
			log = LogManager.GetLogger(this.GetType());
		}

		[HttpGet]
		[Route("Index")]
		[ActionName("Gallery")]
		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		[Route("Category/{category}")]
		public ActionResult Category(int category) {
			return View(GalleryCategoryRepository.find(category));
		}

		[HttpGet]
		[Route("Item/{category}/{itemId}")]
		public ActionResult Item(int category, int itemId) {
			GalleryCategory galleryCategory = GalleryCategoryRepository.find(category);
			foreach (Item item in galleryCategory.Items) {
				if (item.Id == itemId)
					return View(ItemRepository.find(item.Id));
			}
			ModelState.AddModelError(string.Empty, "Unable to find item!");
			return View("Category/" + category, GalleryCategoryRepository.find(category));
		}

		/// <summary>
		/// Gets an image by its Id in the Database
		/// </summary>
		/// <param name="id">Id of <see cref="Image"/></param>
		/// <param name="height">Height of Image to resize to</param>
		/// <param name="width">Width of Image to resize to</param>
		/// <returns></returns>
		[HttpGet]
		[Route("Image/{id}/{height:int?}/{width:int?}")]
		public FileContentResult Image(int id, int height = 0, int width = 0) {
			ItemImage itemImage = ItemImageRepository.find(id);
			if (itemImage.ActiveStatus == ActiveStatus.INACTIVE) {
				log.Error("Unable to get Item_Image; Item_Image is not active!");
				Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return null;
			}
			if (itemImage == null)
				return null;
			else if (height == 0 || width == 0) {
				try {
					using (MemoryStream memoryStream = new MemoryStream(itemImage.Image)) {
						System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
						Graphics graphics = Graphics.FromImage(image);
						if (height != 0 && width == 0)
							width = (int)((height / (float)image.Height) * (float)image.Width);
						else if (width != 0 && height == 0)
							height = (int)((width / (float)image.Width) * (float)image.Height);
						log.Debug("Attempting to resize image to " + height + "H X " + width + "W");
						Bitmap newImage = new Bitmap((int)width, (int)height);
						Graphics.FromImage(newImage).DrawImage(image, 0, 0, (float)width, (float)height);
						Bitmap bitmap = new Bitmap(newImage);
						ImageConverter imageConverter = new ImageConverter();
						return File((byte[])imageConverter.ConvertTo(bitmap, typeof(byte[])), itemImage.MemeType);
					}
				} catch (Exception e) {
					log.Error("Unable to resize image!", e);
				}
			}
			return File(itemImage.Image, itemImage.MemeType);
		}
	}
}