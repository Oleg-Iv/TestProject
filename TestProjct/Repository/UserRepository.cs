using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Models;

namespace TestProject.Repository
{
	public class UserRepository
	{
		// List for user is  potentioly login
		private static readonly List<UserModels> UserList = new List<UserModels>();
		// Use DbContext
		private TestDbContext _db;

		// get user by token
		public UserModels GetByToken(string token)
		{
			var user = UserList.FirstOrDefault(u => u.Token == token);
			return user;
		}

		//get user by UserModels
		public UserModels GetUser(string mail, string password)
		{
			// get user from UserList
			var user = UserList.FirstOrDefault(u => u.Mail == mail && u.Password == password);
			if (user == null)
			{
				 _db = new TestDbContext();
				// get user from database
				user = _db.Users.FirstOrDefault(u => u.Mail == mail && u.Password == password);
				if (user == null)
				{
					return null;
				}
				//create token for this user and add it to userList
				user.Token = Guid.NewGuid().ToString();
				UserList.Add(user);
			}
			return user;
		}

		// get user from list by id
		public UserModels GetFromList(int id)
		{
			return UserList.FirstOrDefault(x => x.Id == id);
		}

		// get user from list by name        
		public UserModels GetFromList(string name)
		{
			return UserList.FirstOrDefault(x => x.Name == name);
		}

		// get user from list by token
		public void DeleteFromList(string token)
		{
			UserList.Remove(UserList.Find(x => x.Token == token));
		}
	}
}