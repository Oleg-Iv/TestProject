using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Hubs
{
	public class MainHub : Hub
	{
		//const of reload page
		public static string ReloadMassegeConst = "Reload";
		public static List<UserModels> ActiveUserList = new List<UserModels>();
		private readonly UserRepository _userRepository = new UserRepository();

		public void Send(string name, string message)
		{
			Clients.User(name).addMessage(name, message);
		}

		public void SendAll(string name, string message)
		{
			Clients.Others.addMessage(name, message);
		}

		/// <summary>
		/// Connect new user.
		/// </summary>
		/// <param name="userName"></param>
		public void Connect(string userName)
		{
			string token = Context.User.Identity.Name;
			if (string.IsNullOrEmpty(token)) return; // can be when user is not login do nuthing.

			UserModels user = _userRepository.GetByToken(token);
			if (user != null && !ActiveUserList.Contains(user))
			{
				//add user to list active users
				ActiveUserList.Add(user);

				//if Muster login in the system send info about this user to him
				UserModels musterUser = ActiveUserList.FirstOrDefault(n => n.Roles == Roles.MusterUser);
				if (musterUser != null)
					Clients.User(musterUser.Token).onNewUserConnected(token, user.Name);
			}
			else
			{
				//something is wrong reload user page
				if (!string.IsNullOrEmpty(token))
					Send(userName, ReloadMassegeConst);
			}
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			int id;
			if (!int.TryParse(Context.User.Identity.Name, out id)) base.OnDisconnected(stopCalled);

			//get info abount log off user
			UserModels user = ActiveUserList.FirstOrDefault(x => x.Id == id);
			if (user != null)
			{
				//remove from ActiveUserList
				ActiveUserList.Remove(user);

				//if Muster login in the system send info about this user to him
				UserModels musterUser = ActiveUserList.FirstOrDefault(n => n.Roles == Roles.MusterUser);
				if (musterUser != null)
					Clients.User(musterUser.Token).onUserDisconnected(id);
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}