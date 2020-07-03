using System.Net.Sockets;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using VirtualCharacterSheet;

namespace VirtualCharacterSheet.Net {

	public static class Host {

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