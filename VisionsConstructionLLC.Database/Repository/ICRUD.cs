using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionsConstructionLLC.Database.Repository {
	public interface ICRUD<T> {

		/// <summary>
		/// Finds an entity by its Key
		/// </summary>
		/// <param name="index"></param>
		/// <returns>Entity</returns>
		T find(int index);

		/// <summary>
		/// Finds all instances of an entity
		/// </summary>
		/// <returns><see cref="List"/> of Entities</returns>
		List<T> findAll();

		/// <summary>
		/// Gets the number of instances in an entity
		/// </summary>
		/// <returns><see cref="Long"/>Number of instances</returns>
		long? count();

		/// <summary>
		/// Creates a new instance of an entity
		/// </summary>
		/// <param name="type">New Instnace</param>
		void create(T type);

		/// <summary>
		/// Updates an existing instance of an entity
		/// </summary>
		/// <param name="type"></param>
		void save(T type);

		/// <summary>
		/// Deletes an existing instance of an entity.
		/// 
		/// NOTE: Should not be used; inactive records instead!
		/// </summary>
		/// <param name="type"></param>
		[Obsolete("Records should be changed to inactive, not deleted.")]
		void delete(T type);
	}
}