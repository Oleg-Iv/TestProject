using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using TestProject.Helpers;
using TestProject.Models;
using System.Threading;
namespace TestProject.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		// Action for Welcome Page
		public ActionResult Index()
		{
			UserModels user = HttpContextHelper.GetcurrentUser();
			if (user == null)
				Response.Redirect("User/logout");
			return View(user);
		}
		
		/// <summary>
		/// method for get random string
		/// </summary>
		/// <returns>string key</returns>
        [HttpPost, ValidateInput(false)]
		public string GetKey(string pablicKey)
		{
			string key = Protection.RandomString(20);
			//// save in sesion for not sending key mor then ones.
			Session["secret"] = key;
			return Encryption(pablicKey, key);
		}

		/// <summary>
		/// method get string encrypted decrypt user string 
		/// </summary>
		/// <param name="encrypted"> encrypted string</param>
		/// <returns>Decrypt string</returns>
		[HttpPost]
		public string Send(string encrypted)
		{
			Thread.Sleep(5000); // sleep 5s
			// user helper Protection
			var p = new Protection();
			//Decrypt by secret key from session
			var s = p.OpenSSLDecrypt(encrypted, Session["secret"].ToString());
			return s;
		}



		UnicodeEncoding ByteConverter = new UnicodeEncoding();
		

		static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
		{
			try
			{
				byte[] encryptedData;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
				{
					RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
				}
				return encryptedData;
			}
			catch (CryptographicException e)
			{
				//Console.WriteLine(e.Message);
				return null;
			}
		}

		static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
		{
			try
			{
				byte[] decryptedData;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
				{
					RSA.ImportParameters(RSAKey);
					decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
				}
				return decryptedData;
			}
			catch (CryptographicException e)
			{
				//Console.WriteLine(e.ToString());
				return null;
			}
		}

		static string GetString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		public string Encryption( string pablicKey, string key)
		{

			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
			{
				//var tt = StringToByteArray("010001");
				try
				{

                    rsa.FromXmlString(pablicKey);
					//rsa.ImportParameters(parameters);

					byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(key), true);

					string base64Encrypted = Convert.ToBase64String(encryptedData);
					Debug.WriteLine(base64Encrypted);

					//server decrypting data with private key

					//  rsa.FromXmlString(privateKey);

					//var resultBytes = Convert.FromBase64String(base64Encrypted);
					//var decryptedBytes = rsa.Decrypt(resultBytes, true);
					//var decryptedData = Convert.ToBase64String(decryptedBytes);
					//Debug.WriteLine(decryptedData);

					return base64Encrypted;
				}
				finally
				{
					rsa.PersistKeyInCsp = false;
				}

			}
		}
		


		public static byte[] StringToByteArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
							 .Where(x => x % 2 == 0)
							 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							 .ToArray();
		}

	}
}
