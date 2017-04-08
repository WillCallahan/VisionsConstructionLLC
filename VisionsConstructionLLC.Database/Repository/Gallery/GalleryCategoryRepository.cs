using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VisionsConstructionLLC.Database.Concrete;
using VisionsConstructionLLC.Database.Entities.Gallery;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository.Gallery {
	public class GalleryCategoryRepository : IGalleryCategoryRepository {
		private ILog log;
		private EntityManagerDbContext context = new EntityManagerDbContext();

		public GalleryCategoryRepository() {
			log = LogManager.GetLogger(this.GetType());
		}

		public GalleryCategory find(int index) {
			log.Debug("Attempting to find an existing GalleryCategory by its Id of " + index);
			return context.GalleryCategory.Include(g => g.Items.Select(i => i.ItemImages)).Single(g => g.Id == index);
		}

		public List<GalleryCategory> findAll(ActiveStatus activeStatus) {
			log.Debug("Attempting to find a GalleryCategory by an Active Status of " + activeStatus);
			return context.GalleryCategory.Include(g => g.Items.Select(z => z.ItemImages)).Where(g => g.ActiveStatusCode == activeStatus.Code).ToList();
		}

		public List<GalleryCategory> findAll() {
			log.Debug("Attempting to find all GalleryCategory...");
			return context.GalleryCategory.Include(g => g.Items.Select(i => i.ItemImages)).ToList();
		}

		public long? count() {
			log.Debug("Attempting to get the count of GalleryCategory...");
			return context.GalleryCategory.Count();
		}

		public long? count(ActiveStatus activeStatus) {
			log.Debug("Attempting to get the count of GalleryCategory with an Active Status of " + activeStatus);
			return context.GalleryCategory.Count(g => g.ActiveStatusCode == activeStatus.Code);
		}

		public void create(GalleryCategory type) {
			log.Info("Attempting to create a new GalleryCategory...");
			context.GalleryCategory.Add(type);
			context.SaveChanges();
		}

		public void saveActiveStatus(GalleryCategory type) {
			log.Info("Attempting to update a GalleryCategory Active Status to " + type.ActiveStatus + " with an Id of " + type.Id);
			GalleryCategory galleryCategory = context.GalleryCategory.Single(g => g.Id == type.Id);
			galleryCategory.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void save(GalleryCategory type) {
			log.Info("Attempting to update an existing GalleryCategory with an Id of " + type.Id);
			GalleryCategory galleryCategory = context.GalleryCategory.Single(g => g.Id == type.Id);
			galleryCategory.Name = type.Name;
			galleryCategory.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void delete(GalleryCategory type) {
			log.Info("Attempting to delete a GalleryCategory with an Id of " + type.Id);
			context.GalleryCategory.Remove(type);
			context.SaveChanges();
		}

		public void delete(ActiveStatus activeStatus) {
			log.Info("Attempting to delete all GalleryCategory with an Active Status of " + activeStatus);
			foreach (int id in context.GalleryCategory.Where(g => g.ActiveStatusCode == activeStatus.Code).Select(g => g.Id)) {
				GalleryCategory galleryCategory = new GalleryCategory() { Id = id };
				context.GalleryCategory.Attach(galleryCategory);
				context.GalleryCategory.Remove(galleryCategory);
			}
			context.SaveChanges();
		}
	}
}