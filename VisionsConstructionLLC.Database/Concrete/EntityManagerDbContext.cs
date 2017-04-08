using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionsConstructionLLC.Database.Entities;
using VisionsConstructionLLC.Database.Entities.Gallery;

namespace VisionsConstructionLLC.Database.Concrete {
	public class EntityManagerDbContext : DbContext {

		public DbSet<Item> Item { get; set; }
		public DbSet<ItemImage> ItemImage { get; set; }
		public DbSet<GalleryCategory> GalleryCategory { get; set; }
		public DbSet<Email> Email { get; set; }
		public DbSet<User> User { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("dbo");
		}

	}
}
