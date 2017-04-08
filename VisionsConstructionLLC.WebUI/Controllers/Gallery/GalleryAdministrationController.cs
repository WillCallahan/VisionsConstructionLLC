using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionsConstructionLLC.Database.Entities.Gallery;
using VisionsConstructionLLC.Database.Models.Enums;
using VisionsConstructionLLC.Database.Repository.Gallery;
using VisionsConstructionLLC.WebUI.Attribute;

namespace VisionsConstructionLLC.WebUI.Controllers.Gallery {

	/// <summary>
	/// Gallery Administration Controller, used to modify
	/// all values related to the Gallery in the database.
	/// </summary>
	[Authorize]
	[RoutePrefix("GalleryAdministration")]
	[LocationLogger]
	public class GalleryAdministrationController : Controller {
		private ILog log;

		[Inject]
		public IItemRepository ItemRepository { private get; set; }
		[Inject]
		public IItemImageRepository ItemImageRepository { private get; set; }
		[Inject]
		public IGalleryCategoryRepository GalleryCategoryRepository { private get; set; }

		public GalleryAdministrationController() {
			log = LogManager.GetLogger(this.GetType());
		}

		/// <summary>
		/// Gets the main Gallery Administration View
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route]
		[Route("Administration")]
		public ActionResult GalleryAdministration() {
			return View(GalleryCategoryRepository.findAll());
		}

		/// <summary>
		/// Creates a new Section (aka <see cref="GalleryCategory"/>)
		/// </summary>
		/// <param name="galleryCategory"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CreateSection(GalleryCategory galleryCategory) {
			if (!ModelState.IsValid)
				return View(galleryCategory);
			GalleryCategoryRepository.create(galleryCategory);
			return RedirectToAction("GalleryAdministration", "GalleryAdministration");
		}

		/// <summary>
		/// Gets the view to create a new Section (aka <see cref="GalleryCategory"/>)
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult CreateSection() {
			return View("ModifySection");
		}

		/// <summary>
		/// Updates an existing <see cref="GalleryCategory"/>
		/// </summary>
		/// <param name="galleryCategory"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult EditGalleryCategory(GalleryCategory galleryCategory) {
			if (!ModelState.IsValid || galleryCategory == null) {
				ModelState.AddModelError(String.Empty, "Invalid Section. Please try again.");
				return View("SectionAndItemModify", galleryCategory);
			}
			else {
				GalleryCategoryRepository.save(galleryCategory);
				TempData["Message"] = "Successfully edited category!";
				return RedirectToAction("GalleryAdministration", "GalleryAdministration");
			}
		}

		/// <summary>
		/// Responds to a request to modify or delete an existing
		/// Gallery Category. If the submit action is to edit, then
		/// the Editing View is returned with the related Gallery Category
		/// </summary>
		/// <param name="submitAction"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("ModifySelection/{id}")]
		public ActionResult ModifySelection(int? id, string submitAction = "edit") {
			if (id == null) {
				log.Warn("ID not provided!");
				ModelState.AddModelError(String.Empty, "Invalid Id!");
			}
			else if (submitAction == null) {
				log.Warn("BusmitAction not provided!");
				ModelState.AddModelError(String.Empty, "Invalid Submission Type!");
			}
			else if (submitAction == "delete") {
				GalleryCategory galleryCategory = GalleryCategoryRepository.find((int)id);
				if (galleryCategory == null)
					ModelState.AddModelError(String.Empty, "Unable to find Section to delete!");
				else {
					galleryCategory.ActiveStatus = ActiveStatus.INACTIVE;
					GalleryCategoryRepository.saveActiveStatus(galleryCategory);
				}
			}
			else if (submitAction == "add") {
				GalleryCategory galleryCategory = GalleryCategoryRepository.find((int)id);
				if (galleryCategory == null)
					ModelState.AddModelError(String.Empty, "Unable to find Section to delete!");
				else {
					galleryCategory.ActiveStatus = ActiveStatus.ACTIVE;
					GalleryCategoryRepository.saveActiveStatus(galleryCategory);
				}
			}
			else if (submitAction == "edit")
				return View("SectionAndItemModify", GalleryCategoryRepository.find((int)id));
			return View("GalleryAdministration", GalleryCategoryRepository.findAll());
		}

		/// <summary>
		/// Gets the SectionAndItemModify View with a Model based on the ID URL Parameter
		/// </summary>
		/// <param name="id">ID of <see cref="GalleryCategory"/></param>
		/// <returns></returns>
		[HttpGet]
		[Route("ModifySelection/{id}")]
		public ActionResult ModifySelection(int? id) {
			if (id == null) {
				log.Warn("ID not provided!");
				ModelState.AddModelError(String.Empty, "Invalid Id!");
				return View("GalleryAdministration", GalleryCategoryRepository.findAll());
			}
			else
				return View("SectionAndItemModify", GalleryCategoryRepository.find((int)id));
		}

		/// <summary>
		/// Gets the View to Create a new Item
		/// </summary>
		/// <param name="galleryCategory"><see cref="GalleryCategory"/>Related to Item</param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult CreateItem(int? id) {
			if (id == null) {
				log.Warn("ID not provided!");
				ModelState.AddModelError(String.Empty, "Unable to find category being edited. Please try again.");
				return RedirectToAction("GalleryAdministration", "GalleryAdministration");
			}
			else
				return View(new Item() { GalleryCategoryId = (int)id });
		}

		/// <summary>
		/// Creates a new <see cref="Item"/> and returns the Item Modification
		/// View if the Item can be created.
		/// </summary>
		/// <param name="item"><see cref="Item"/> to create</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CreateItem(Item item) {
			if (ModelState.IsValid) {
				ItemRepository.create(item);
				return RedirectToAction("ModifyItem", item);
			}
			else
				return View(item);
		}

		/// <summary>
		/// Retuns the view to modify an exsisting <see cref="Item"/>
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("ModifyItem/{id}")]
		public ActionResult ModifyItem(int? id) {
			if (id == null)
				return View();
			else {
				Item item = ItemRepository.find((int)id);
				return View(item);
			}
		}

		/// <summary>
		/// Allows the user to make a choice to UPDATE or DELETE an Item
		/// </summary>
		/// <param name="id">Id of <see cref="Item"/> to be modified</param>
		/// <param name="gallery">Id of <see cref="GallerySection"/></param>
		/// <param name="submitAction">View</param>
		/// <returns></returns>
		[HttpPost]
		[Route("ModifyItemAction/{id}/{gallery}")]
		public ActionResult ModifyItemAction(int? id, int? gallery, string submitAction = "edit") {
			if (id == null) {
				log.Warn("ID not provided!");
				ModelState.AddModelError(String.Empty, "Unable to " + submitAction + " item!");
				return View("GalleryAdministration", GalleryCategoryRepository.findAll());
			}
			else if (gallery == null) {
				log.Warn("ID not provided!");
				ModelState.AddModelError(String.Empty, "Unable to get section Id!");
				return View("GalleryAdministration", GalleryCategoryRepository.findAll());
			}
			else if (submitAction == "edit")
				return RedirectToAction("ModifyItem", new { id = id });
			else if (submitAction == "delete") {
				Item item = ItemRepository.find((int)id);
				if (item == null)
					ModelState.AddModelError(String.Empty, "Unable to find item; could not delete!");
				else {
					item.ActiveStatus = ActiveStatus.INACTIVE;
					ItemRepository.saveActiveStatus(item);
				}
			}
			else if (submitAction == "add") {
				Item item = ItemRepository.find((int)id);
				if (item == null)
					ModelState.AddModelError(String.Empty, "Unable to find item; could not add!");
				else {
					item.ActiveStatus = ActiveStatus.ACTIVE;
					ItemRepository.saveActiveStatus(item);
				}
			}
			return View("SectionAndItemModify", GalleryCategoryRepository.find((int)gallery));
		}

		/// <summary>
		/// Modifies an exsiting item
		/// </summary>
		/// <param name="item"><see cref="Item"/> to modify</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ModifyItem(Item item) {
			if (!ModelState.IsValid)
				return View(item);
			ItemRepository.save(item);
			TempData["Message"] = "Successfully updated item!";
			return View(item);
		}

		/// <summary>
		/// Creates a new set of Images in the database
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Image(Item item) {
			ItemRepository.save(item);
			for (int i = 0; i < Request.Files.Count; i++ ) {
				HttpPostedFileBase httpPostedFileBase = Request.Files[i];
				if (httpPostedFileBase.ContentLength == 0)
					continue;
				ItemImage itemImage = new ItemImage();
				itemImage.ItemId = item.Id;
				itemImage.MemeType = httpPostedFileBase.ContentType;
				itemImage.Image = new Byte[httpPostedFileBase.ContentLength];
				httpPostedFileBase.InputStream.Read(itemImage.Image, 0, httpPostedFileBase.ContentLength);
				ItemImageRepository.create(itemImage);
			}
			return View("ModifyItem", ItemRepository.find(item.Id));
		}

		/// <summary>
		/// Sets the <see cref="ActiveStatus"/> of an <see cref="ItemImage"/>
		/// to INACTIVE
		/// </summary>
		/// <param name="item">Item being modified</param>
		/// <param name="imageId">Id of <see cref="ItemImage"/> to be removed</param>
		/// <returns></returns>
		[HttpPost]
		[Route("DeleteImage")]
		public ActionResult DeleteImage(Item item, int imageId) {
			ModelState.Clear();
			ItemImage itemImage = ItemImageRepository.find(imageId);
			itemImage.ActiveStatus = ActiveStatus.INACTIVE;
			ItemImageRepository.saveActiveStatus(itemImage);
			item = ItemRepository.find(item.Id);
			return View("ModifyItem", item);
		}

		/// <summary>
		/// Sets the <see cref="ActiveStatus"/> of an <see cref="ItemImage"/>
		/// to ACTIVE
		/// </summary>
		/// <param name="item">Item being modified</param>
		/// <param name="imageId">Id of <see cref="ItemImage"/> to be removed</param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddImage")]
		public ActionResult AddImage(Item item, int imageId) {
			ModelState.Clear();
			ItemImage itemImage = ItemImageRepository.find(imageId);
			itemImage.ActiveStatus = ActiveStatus.ACTIVE;
			ItemImageRepository.saveActiveStatus(itemImage);
			item = ItemRepository.find(item.Id);
			return View("ModifyItem", item);
		}
	}
}