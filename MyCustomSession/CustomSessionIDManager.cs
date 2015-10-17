using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Linq;

namespace MyCustomSession
{
	public class CustomSessionIDManager : SessionIDManager
	{
		public const int KEY_LENGTH = 64;
		private char[] Encoding = {
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5' };


		public override string CreateSessionID(HttpContext context)
		{
			char[] identifier = new char[KEY_LENGTH];
			byte[] randomData = new byte[KEY_LENGTH];

			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(randomData);
			}

			for (int i = 0; i < identifier.Length; i++)
			{
				int pos = randomData[i] % Encoding.Length;
				identifier[i] = Encoding[pos];
			}

			return new string(identifier);
		}

		public override bool Validate(string id)
		{
			try
			{
				if (id.Length != KEY_LENGTH)
				{
					return false;
				}

				for (int i = 0; i < id.Length; i++)
				{
					if (!Encoding.Contains(id[i]))
					{
						return false;
					}
				}

				return true;
			}
			catch
			{
			}

			return false;
		}
	}
}
