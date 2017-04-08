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
	public class ItemRepository : IItemRepository {
		private ILog log;
		private EntityManagerDbContext context = new EntityManagerDbContext();

		public ItemRepository() {
			log = LogManager.GetLogger(this.GetType());
		}

		public Item find(int index) {
			log.Debug("Attempting to find an Item by its Id of " + index);
			return context.Item.Include(i => i.ItemImages).Single(g => g.Id == index);
		}

		public List<Item> findAll(ActiveStatus activeStatus) {
			log.Debug("Attempting to find all Item with an Active Status of " + activeStatus);
			return context.Item.Include(i => i.ItemImages).Where(i => i.ActiveStatusCode == activeStatus.Code).OrderByDescending(item => item.Id).ToList();
		}

		public List<Item> findAll() {
			log.Debug("Attempting to find all Item...");
			return context.Item.Include(i => i.ItemImages).OrderByDescending(item => item.Id).ToList();
		}

		public long? count(ActiveStatus activeStatus) {
			log.Debug("Attempting to get the count of Item with an Active Status of " + activeStatus);
			return context.Item.Count(i => i.ActiveStatusCode == activeStatus.Code);
		}

		public long? count() {
			log.Debug("Attempting to get the count of Item...");
			return context.Item.Count();
		}

		public void saveActiveStatus(Item type) {
			log.Info("Attempting to update an Item Active Status to " + type.ActiveStatus);
			Item item = context.Item.Single(i => i.Id == type.Id);
			item.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void save(Item type) {
			log.Info("Attempting to update an Item..." );
			Item item = context.Item.Single(i => i.Id == type.Id);
			item.Title = type.Title;
			item.ShowcaseDescription = type.ShowcaseDescription;
			item.Description = type.Description;
			item.GalleryCategoryId = type.GalleryCategoryId;
			item.ActiveStatus = type.ActiveStatus;
			context.SaveChanges();
		}

		public void create(Item type) {
			log.Info("Attempting to create a new Item...");
			context.Item.Add(type);
			context.SaveChanges();
		}

		public void delete(Item type) {
			log.Info("Attempting to delete an Item...");
			context.Item.Remove(type);
			context.SaveChanges();
		}

		public void delete(ActiveStatus activeStatus) {
			log.Info("Attempting to delete all Item with an Active Status of " + activeStatus);
			foreach (int id in context.Item.Where(i => i.ActiveStatusCode == activeStatus.Code).Select(i => i.Id)) {
				Item item = new Item() { Id = id };
				context.Item.Attach(item);
				context.Item.Remove(item);
			}
			context.SaveChanges();
		}
	}
}