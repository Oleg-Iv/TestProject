using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestProject.Helpers
{
	[Obsolete]
	public static class AESHelper
	{
		public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			byte[] encryptedBytes = null;
			// Set your salt here, change it to meet your flavor:
			var saltBytes = passwordBytes;
			// Example:
			//saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (var ms = new MemoryStream())
			{
				using (var AES = new RijndaelManaged())
				{
					AES.KeySize = 256;
					AES.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					AES.Key = key.GetBytes(AES.KeySize/8);
					AES.IV = key.GetBytes(AES.BlockSize/8);

					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						cs.Close();
					}
					encryptedBytes = ms.ToArray();
				}
			}

			return encryptedBytes;
		}

		public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			byte[] decryptedBytes = null;
			// Set your salt here to meet your flavor:
			var saltBytes = passwordBytes;
			// Example:
			//saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (var ms = new MemoryStream())
			{
				using (var AES = new RijndaelManaged())
				{
					AES.KeySize = 256;
					AES.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					AES.Key = key.GetBytes(AES.KeySize/8);
					AES.IV = key.GetBytes(AES.BlockSize/8);

					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						cs.Close();
					}
					decryptedBytes = ms.ToArray();
				}
			}

			return decryptedBytes;
		}

		public static string Encrypt(string text, byte[] passwordBytes)
		{
			var originalBytes = Encoding.UTF8.GetBytes(text);
			byte[] encryptedBytes = null;

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			// Getting the salt size
			var saltSize = GetSaltSize(passwordBytes);
			// Generating salt bytes
			var saltBytes = GetRandomBytes(saltSize);

			// Appending salt bytes to original bytes
			var bytesToBeEncrypted = new byte[saltBytes.Length + originalBytes.Length];
			for (var i = 0; i < saltBytes.Length; i++)
			{
				bytesToBeEncrypted[i] = saltBytes[i];
			}
			for (var i = 0; i < originalBytes.Length; i++)
			{
				bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
			}

			encryptedBytes = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

			//return Encoding.Unicode.GetString(encryptedBytes);
			return Convert.ToBase64String(passwordBytes);
		}

		public static string Decrypt(string decryptedText, byte[] passwordBytes)
		{
			var bytesToBeDecrypted = Convert.FromBase64String(decryptedText);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var decryptedBytes = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

			// Getting the size of salt
			var saltSize = GetSaltSize(passwordBytes);

			// Removing salt bytes, retrieving original bytes
			var originalBytes = new byte[decryptedBytes.Length - saltSize];
			for (var i = saltSize; i < decryptedBytes.Length; i++)
			{
				originalBytes[i - saltSize] = decryptedBytes[i];
			}

			return Encoding.UTF8.GetString(originalBytes);
		}

		public static int GetSaltSize(byte[] passwordBytes)
		{
			var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
			var ba = key.GetBytes(2);
			var sb = new StringBuilder();
			for (var i = 0; i < ba.Length; i++)
			{
				sb.Append(Convert.ToInt32(ba[i]).ToString());
			}
			var saltSize = 0;
			var s = sb.ToString();
			foreach (var c in s)
			{
				var intc = Convert.ToInt32(c.ToString());
				saltSize = saltSize + intc;
			}

			return saltSize;
		}

		public static byte[] GetRandomBytes(int length)
		{
			var ba = new byte[length];
			RandomNumberGenerator.Create().GetBytes(ba);
			return ba;
		}
	}
}