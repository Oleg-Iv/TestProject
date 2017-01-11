using System.Web.Mvc;
using TestProject.Helpers;
using TestProject.Hubs;
using TestProject.Models;

namespace TestProject.Controllers
{
	public class MasterPageController : Controller
	{
		// Action for muster page
		public ActionResult Index()
		{
			// check is user is Master
			if (HttpContextHelper.GetcurrentUser().Roles != Roles.MusterUser)
			{
				return RedirectToAction("Index", "Home");
			}

			return View(MainHub.ActiveUserList);
		}

	}
}
