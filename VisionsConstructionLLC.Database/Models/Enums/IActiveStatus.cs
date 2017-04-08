using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionsConstructionLLC.Database.Models.Enums {
	public interface IActiveStatus {

		/// <summary>
		/// Actual <see cref="ActiveStatus"/> Object
		/// </summary>
		ActiveStatus ActiveStatus { get; set; }

		/// <summary>
		/// Code of the local ActiveStatus field, representing
		/// a Code in the <see cref="ActiveStatus"/> object.
		/// </summary>
		int ActiveStatusCode { get; set; }
	}
}
