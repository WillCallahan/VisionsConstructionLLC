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
	public class UserRepository : IUserRepository {
		private ILog log;
		private EntityManagerDbContext context = new EntityManagerDbContext();

		public UserRepository() {
			log = LogManager.GetLogger(this.GetType());
		}

		public User find(int index) {
			log.Debug("Attempting to find an existing User by its Id of " + index);
			return context.User.Where(u => u.Id == index).Single();
		}

		public User find(String username, String password) {
			log.Debug("Attempting to find an existing User by its username of " + username);
			return context.User.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).Single();
		}

		public List<User> findAll(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public List<User> findAll() {
			throw new NotImplementedException();
		}

		public long? count() {
			throw new NotImplementedException();
		}

		public long? count(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public void create(User type) {
			throw new NotImplementedException();
		}

		public void save(User type) {
			throw new NotImplementedException();
		}

		public void saveActiveStatus(User type) {
			throw new NotImplementedException();
		}

		public void delete(ActiveStatus activeStatus) {
			throw new NotImplementedException();
		}

		public void delete(User type) {
			throw new NotImplementedException();
		}
	}
}
