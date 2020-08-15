using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace VirtualCharacterSheet.Net.API {

	[ApiController]
	[Route("character")]
	public sealed class CharacterController : ControllerBase{

		[HttpGet("{identity}")]
		public HttpResponseMessage Get(string identity) {
			if(!Data.HasCharacter(identity))
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var character = Data.GetCharacter(identity);
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			PlayerCharacter.Serialize(character, writer, shouldclose: false);
			stream.Position = 0;
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			writer.Flush();
			return result;
		}

	}

}