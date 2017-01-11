using System.Web.Mvc;
using TestProjct.Helpers;
using TestProjct.Hubs;
using TestProjct.Models;

namespace TestProjct.Controllers
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
