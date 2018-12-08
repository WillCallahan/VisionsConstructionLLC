using System;
using System.Security.Cryptography;
using System.Text;
using log4net;

namespace VisionsConstructionLLC.Common.Crypto {

	/// <inheritdoc/>
	public class Sha2Hasher : IHasher {

		private readonly ILog _log;

		public Sha2Hasher() {
			_log = LogManager.GetLogger(this.GetType());
		}

		/// <inheritdoc/>
		public string Hash(string value) {
			_log.Debug("Encrypting value using SHA512 algorithm");

			var sha2 = SHA512.Create();
			sha2.ComputeHash(Encoding.UTF8.GetBytes(value));

			var hash = BitConverter.ToString(sha2.Hash);

			_log.Debug("Value encrypted.  Hash=" + hash);
			return hash;
		}

	}
}
