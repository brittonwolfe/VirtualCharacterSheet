using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace VirtualCharacterSheet.Net.API {

	public sealed class CharacterController {
		private List<MemoryStream> Streams = new List<MemoryStream>();

		[HttpGet("/character/{identity}")]
		public HttpResponseMessage Get(string identity) {
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var character = Data.GetCharacter(identity);
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			PlayerCharacter.Serialize(character, writer);
			stream.Position = 0;
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			Streams.Add(stream);
			return result;
		}

		public void DisposeAll() {
			foreach(MemoryStream stream in Streams)
				stream.Close();
			Streams.Clear();
		}

	}

}