using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository {
	public interface ICRUDActiveStatus<T> : ICRUD<T> where T : IActiveStatus {

		/// <summary>
		/// Finds all instances of an entity with a specific <see cref="ActiveStatus"/>
		/// </summary>
		/// <param name="activeStatus"></param>
		/// <returns></returns>
		List<T> findAll(ActiveStatus activeStatus);

		/// <summary>
		/// Gets the number of available itmes with a specific <see cref="ActiveStatus"/>
		/// </summary>
		/// <param name="activeStatus"></param>
		/// <returns></returns>
		long? count(ActiveStatus activeStatus);

		/// <summary>
		/// Saves the <see cref="ActiveStatus"/> of a particular entity
		/// </summary>
		/// <param name="type"></param>
		void saveActiveStatus(T type);

		/// <summary>
		/// Deletes all indexed with a specific <see cref="ActiveStats"/>
		/// 
		/// NOTE: This should not be used; instead deactive items.
		/// </summary>
		/// <param name="activeStatus"></param>
		[Obsolete("Records should be changed to inactive, not deleted.")]
		void delete(ActiveStatus activeStatus);
	}
}
