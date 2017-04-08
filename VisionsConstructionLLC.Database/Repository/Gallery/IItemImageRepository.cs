using System;
using System.Collections.Generic;
using VisionsConstructionLLC.Database.Entities.Gallery;
using VisionsConstructionLLC.Database.Models.Enums;

namespace VisionsConstructionLLC.Database.Repository.Gallery {
	public interface IItemImageRepository : ICRUDActiveStatus<ItemImage> {

		/// <summary>
		/// Gets the number of <see cref="ItemItem"/> with
		/// a specific Id from a <see cref="Item"/>
		/// </summary>
		/// <param name="index">Index of <see cref="Item"/></param>
		/// <returns>Number of <see cref="ItemImage"/> with a specific ItemId</returns>
		long? count(int index);

		/// <summary>
		/// Gets the number of <see cref="ItemItem"/> with
		/// a specific Id from a <see cref="Item"/> and a
		/// specific <see cref="ActiveStatus"/>
		/// </summary>
		/// <param name="index">Index of <see cref="Item"/></param>
		/// <param name="activeStatus"><see cref="ActiveStatus"/> of the Image</param>
		/// <returns>Number of <see cref="ItemImage"/> with a specific ItemId</returns>
		long? count(int index, ActiveStatus activeStatus);

	}
}
