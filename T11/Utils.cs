using System;
using System.Security.Cryptography;
using System.Text;

namespace T11
{
	public class Utils
	{
		public Utils()
		{
		}

		public static string Hash(string text, string alg)
		{
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = null;
            // MD5
            if (alg.ToLower().Equals("md5"))
			{
                MD5 md5 = MD5.Create();
                hashBytes = md5.ComputeHash(bytes);
            } else if (alg.ToLower().Equals("sha512"))
            {
                SHA512 sha = SHA512.Create();
                hashBytes = sha.ComputeHash(bytes);
            }
            
            return Convert.ToHexString(hashBytes);
		}
	}
}

