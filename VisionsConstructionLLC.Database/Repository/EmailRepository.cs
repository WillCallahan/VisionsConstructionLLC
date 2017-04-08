using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionsConstructionLLC.Database.Concrete;
using VisionsConstructionLLC.Database.Entities;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository {
	public class EmailRepository : IEmailRepository {
		private ILog log;
		private EntityManagerDbContext context = new EntityManagerDbContext();

		public EmailRepository() {
			log = LogManager.GetLogger(this.GetType());
		}

		public List<Email> findAll(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public long? count(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public void save(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public void deleteAll(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public Email find(int index) {
			return context.Email.Find(index);
		}

		public long? count() {
			throw new NotImplementedException();
		}

		public void create(Email type) {
			log.Info("Attempting to create a new Email...");
			context.Email.Add(type);
			context.SaveChanges();
		}

		public void save(Email type) {
			throw new NotImplementedException();
		}

		public void delete(Email type) {
			throw new NotImplementedException();
		}

		public List<Email> findAll() {
			log.Debug("Attempting to find all Email...");
			return context.Email.OrderByDescending(e => e.Id).ToList<Email>();
		}

	}
}
