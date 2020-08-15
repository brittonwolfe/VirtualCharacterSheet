using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VirtualCharacterSheet.Net.API {

	[ApiController]
	[Route("character")]
	public sealed class CharacterController : ControllerBase{

		[HttpGet("{identity}/bin")]
		public IActionResult GetBin(string identity) {
			if(!Data.HasCharacter(identity))
				return NotFound();
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var character = Data.GetCharacter(identity);
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			PlayerCharacter.Serialize(character, writer, shouldclose: false);
			stream.Position = 0;
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			writer.Flush();
			return File(stream, "application/octet-stream", fileDownloadName: $"{identity.Replace(':','_')}.vcs");
		}

	}

}