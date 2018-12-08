namespace VisionsConstructionLLC.Common.Crypto {
	/// <summary>
	/// Hashes a string
	/// </summary>
	public interface IHasher {

		/// <summary>
		/// Creates a hash of a string
		/// </summary>
		/// <param name="value">Value to hash</param>
		/// <returns>Hashed value</returns>
		string Hash(string value);
	}
}
