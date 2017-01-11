using System.Web.Mvc;
using TestProjct.Helpers;
using TestProjct.Models;
using System.Threading;
namespace TestProjct.Controllers
{
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
		[HttpPost]
		public string GetKey()
		{
			string key = Protection.RandomString(20);

			// save in sesion for not sending key mor then ones.
			Session["secret"] = key; 
			return key;
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
	}
}
