using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionsConstructionLLC.Database.Entities;

namespace VisionsConstructionLLC.Database.Repository {
	public interface IUserRepository : ICRUDActiveStatus<User> {

		/// <summary>
		/// Finds a User by the user's Username and Password
		/// </summary>
		/// <param name="username">Username of User</param>
		/// <param name="password">Password of User</param>
		/// <returns>Matching User</returns>
		User find(String username, String password);

	}
}
