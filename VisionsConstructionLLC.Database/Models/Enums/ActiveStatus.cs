using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisionsConstructionLLC.Database.Models.Enums {
	public sealed class ActiveStatus : IComparable<ActiveStatus>, IEquatable<ActiveStatus> {
		public static readonly ActiveStatus INACTIVE = new ActiveStatus("INACTIVE", 0);
		public static readonly ActiveStatus ACTIVE = new ActiveStatus("ACTIVE", 1);
		public static readonly ActiveStatus UNKNOWN = new ActiveStatus("UNKNOWN", 2);
		private readonly String name;

		private readonly int code;

		private ActiveStatus() {

		}

		private ActiveStatus(String name, int code) {
			this.name = name;
			this.code = code;
		}

		public static IEnumerable<ActiveStatus> Values {
			get {
				yield return INACTIVE;
				yield return ACTIVE;
				yield return UNKNOWN;
			}
		}

		public int CompareTo(ActiveStatus activeStatus) {
			if (this.code < activeStatus.Code)
				return -1;
			else if (this.code == activeStatus.Code)
				return 0;
			else
				return 1;
		}

		public String Name {
			get {
				return name;
			}
		}

		public int Code {
			get {
				return code;
			}
		}

		public bool Equals(ActiveStatus other) {
			if (other == null)
				return false;
			return Code == other.Code;
		}

		public override string ToString() {
			return Name;
		}
	}
}