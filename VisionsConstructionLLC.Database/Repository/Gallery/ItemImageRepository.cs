using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisionsConstructionLLC.Database.Concrete;
using VisionsConstructionLLC.Database.Entities.Gallery;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository.Gallery {
	public class ItemImageRepository : IItemImageRepository {
		private ILog log;
		private EntityManagerDbContext context = new EntityManagerDbContext();

		public ItemImageRepository() {
			log = LogManager.GetLogger(this.GetType());
		}

		public ItemImage find(int index) {
			log.Debug("Attempting to find an exsiting Item Image by its Id of " + index);
			return context.ItemImage.Find(index);
		}

		public List<ItemImage> findAll(ActiveStatus activeStatus) {
			log.Debug("Attempting to find all Item Image by an Active Status of " + activeStatus);
			return context.ItemImage.Where(i => i.ActiveStatus == activeStatus).ToList();
		}

		public List<ItemImage> findAll() {
			log.Debug("Attempting to finad all Item Image...");
			return context.ItemImage.ToList();
		}

		public long? count() {
			log.Debug("Attempting to get the count of Item Image...");
			return context.ItemImage.Count();
		}

		public long? count(int index) {
			log.Debug("Attempting to get the count of Item Image with a Item Id of " + index);
			return context.ItemImage.Where(i => i.ItemId == index).Count();
		}

		public long? count(int index, ActiveStatus activeStatus) {
			log.Debug("Attempting to get the count of Item Image with a Item Id of " + index + " and an Active Status of " + activeStatus);
			return context.ItemImage.Where(i => i.ItemId == index && i.ActiveStatusCode == activeStatus.Code).Count();
		}

		public long? count(ActiveStatus activeStatus) {
			log.Debug("Attempting to get the count of Item Image with an Active Status of " + activeStatus);
			return context.ItemImage.Where(i => i.ActiveStatusCode == activeStatus.Code).Count();
		}

		public void saveActiveStatus(ItemImage type) {
			log.Info("Attempting to udpate an Item Image Active Status to " + type.ActiveStatus);
			ItemImage itemImage = context.ItemImage.Where(i => i.Id == type.Id).Single();
			itemImage.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void delete(ActiveStatus activeStatus) {
			log.Info("Attempting to delete all Item Image with an Active Status of " + activeStatus);
			foreach (int id in context.ItemImage.Where(i => i.ActiveStatusCode == activeStatus.Code).Select(i => i.Id)) {
				ItemImage itemImage = new ItemImage() { Id = id };
				context.ItemImage.Attach(itemImage);
				context.ItemImage.Remove(itemImage);
			}
			context.SaveChanges();
		}

		public void create(ItemImage type) {
			log.Info("Attempting to create a new Item Image...");
			context.ItemImage.Add(type);
			context.SaveChanges();
		}

		public void save(ItemImage type) {
			log.Info("Attempting to update an existing Item Image...");
			ItemImage itemImage = context.ItemImage.Where(i => i.Id == type.Id).Single<ItemImage>();
			itemImage.Image = type.Image;
			itemImage.MemeType = type.MemeType;
			itemImage.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void delete(ItemImage type) {
			log.Info("Attempting to delete an Item Image...");
			context.ItemImage.Remove(type);
			context.SaveChanges();
		}
	}
}