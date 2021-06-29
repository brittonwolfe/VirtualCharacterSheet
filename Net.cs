using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VCS.Net {

	public static class ApiHost {

		public static void StartNetShell() {
			Scripting.engine.ExecuteFile(FileLoad.WorkingDirectory().Get("core/net.py").Path, Scripting.Scope);
			Scripting.Scope.GetVariable("shell")(Scripting.Scope.GetVariable("netshell"));
		}

		public static Task StartHost(string[] args = null) {
			var host = new Task(() => Start(args ?? new string[0]));
			host.Start();
			return host;
		}

		internal static void Start(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		internal static IHostBuilder CreateHostBuilder(string[] args) {
			return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(WebHostBuilder => { WebHostBuilder.UseStartup<Startup>(); });
		}

	}

	public class Startup {

		public void ConfigureServices(IServiceCollection services) {
			services.AddRazorPages();
			services.AddServerSideBlazor();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if(env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseStaticFiles();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
				);
				endpoints.MapFallbackToPage("/_404");
			});
		}
	}

}