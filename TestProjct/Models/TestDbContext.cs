using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestProject.Models
{
	/* A TestDbContext instance represents a combination of the Unit Of Work and 
     * Repository patterns such that it can be used to query from a database and 
     * group together changes that will then be written back to the store as a unit*/
	public class TestDbContext : DbContext
	{
		public TestDbContext()
			: base("TestDbContext")
		{
			Database.SetInitializer(new TestDbInitializer());

		}

		protected override void OnModelCreating(DbModelBuilder mb)
		{
			// Code here
		}


		// virtual collection used to get and set data base  
		public virtual DbSet<UserModels> Users { get; set; }
	}

	public class TestDbInitializer : DropCreateDatabaseIfModelChanges<TestDbContext>
	{
		protected override void Seed(TestDbContext context)
		{

			if (context.Users.Count()== 0)
			{
				UserModels user1 = new UserModels
				{
					Mail = "test@mail.com",
					Name = "Muster",
					Roles = Roles.MusterUser,
					Password = "Password"
				};
				UserModels user2 = new UserModels
				{
					Mail = "test1@mail.com",
					Name = "User1",
					Roles = Roles.User,
					Password = "Password"
				};
				UserModels user3 = new UserModels
				{
					Mail = "test2@mail.com",
					Name = "User2",
					Roles = Roles.User,
					Password = "Password"
				};
				List<UserModels> listUser = new List<UserModels> {user1, user2, user3};

				context.Users.AddRange(listUser);
				context.SaveChanges();
			}
			base.Seed(context);

		}
	}
}
