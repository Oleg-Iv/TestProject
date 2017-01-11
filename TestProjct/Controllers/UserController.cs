using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TestProject.Helpers;
using TestProject.Hubs;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Controllers
{
	public class UserController : Controller
	{
		private readonly UserRepository _userRepository = new UserRepository();
		// Action for view page Login
		[AllowAnonymous]
		public ActionResult Login()
		{
			return View();
		}
		// Action for process form Login
		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(UserModels model)
		{
			if (HttpContextHelper.IsAutorize)
			{
				return RedirectToAction("Index", "Home");
			}
			UserModels user = _userRepository.GetUser(model.Mail, model.Password);
			if (user == null)
			{
				ViewBag.error = "Invalid Email or Password";
				return View();
			}
			FormsAuthentication.SetAuthCookie(user.Token, false);
			return RedirectToAction("Index", "Home");
		}
		// Action for Logout and redirect in Home 
		public ActionResult Logout()
		{
			MainHub.ActiveUserList.Remove(MainHub.ActiveUserList.FirstOrDefault(n => n.Token == HttpContextHelper.UserToken));
			FormsAuthentication.SignOut();
			_userRepository.DeleteFromList(HttpContextHelper.UserToken);
			return RedirectToAction("Index", "Home");
		}
	}
}
