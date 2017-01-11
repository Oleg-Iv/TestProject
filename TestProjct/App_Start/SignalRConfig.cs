using Owin;

//[assembly: OwinStartup(typeof(TestProjct.Startup))]
namespace TestProjct
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}