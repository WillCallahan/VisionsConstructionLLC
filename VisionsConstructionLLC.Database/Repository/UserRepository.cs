using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using VisionsConstructionLLC.Common.Crypto;
using VisionsConstructionLLC.Database.Concrete;
using VisionsConstructionLLC.Database.Entities;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository {
	public class UserRepository : IUserRepository {
		private readonly ILog _log;
		private readonly EntityManagerDbContext _context = new EntityManagerDbContext();
		private readonly IHasher _hasher;

		public UserRepository(IHasher hasher) {
			_log = LogManager.GetLogger(this.GetType());
			_hasher = hasher;
		}

		public User find(int index) {
			_log.Debug("Attempting to find an existing User by its Id of " + index);
			return _context.User.Single(u => u.Id == index);
		}

		public User find(String username, String password) {
			_log.Debug("Attempting to find an existing User by its username of " + username);
			var hashedPassword = _hasher.Hash(password);
			return _context.User.Single(u => u.Username.Equals(username) && u.Password.Equals(hashedPassword));
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
