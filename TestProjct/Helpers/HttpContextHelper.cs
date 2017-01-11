using System.Web;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Helpers
{
	public static class HttpContextHelper
	{
		// UserToken a unique string for the user
		public static string UserToken
		{
			get
			{
				if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
				{
					return HttpContext.Current.User.Identity.Name;
				}
				return null;
			}
		}

		// IsAutorize check in Login
		public static bool IsAutorize
		{
			get { return (!string.IsNullOrEmpty(UserToken)); }
		}

		// GetcurrentUser - get curen user 
		public static UserModels GetcurrentUser()
		{
			var repository = new UserRepository(); // Use UserRepository for get user by list repository
			return repository.GetByToken(UserToken);
		}
	}
}