using Owin;

//[assembly: OwinStartup(typeof(TestProject.Startup))]
namespace TestProject
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}