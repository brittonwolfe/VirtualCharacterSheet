using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

using VirtualCharacterSheet;

namespace VirtualCharacterSheet.Net {

	public static class AppHost {

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
			app.UseRouting();
			app.UseEndpoints(endpoints => {
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/Index");
			});
		}
	}

	internal static class Client {

	}

	public class ClientConnection {
		public readonly string Url;
		private readonly HttpClient client;

		public ClientConnection(string url) {
			Url = url;
			client = new HttpClient();
		}

		public (int, dynamic) MakeRequest(string request, string method = "GET") {
			int code;
			dynamic obj;
			switch(method) {
			case "GET":
				var response = client.GetAsync(request).Result;
				code = (int)response.StatusCode;
				string content = response.Content.ToString();
				obj = JsonConvert.DeserializeObject(content);
				break;
			default:
				throw new System.Exception();
			}
			return (code, obj);
		}

	}

}