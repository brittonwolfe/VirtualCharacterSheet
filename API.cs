using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace VirtualCharacterSheet.Net.API {

	public static class Misc {
		internal static Dictionary<string, dynamic> Handlers = new Dictionary<string, dynamic>();

		public static bool HasHandle(string type) { return Handlers.ContainsKey(type); }

	}

	[ApiController]
	[Route("character")]
	public sealed class CharacterController : ControllerBase {

		[HttpGet("{identity}")]
		public IActionResult GetAction(string identity) {
			if(!Data.HasCharacter(identity))
				return NotFound();
			var character = Data.GetCharacter(identity);
			return new JsonResult(character);
		}

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

	[ApiController]
	[Route("brew")]
	public sealed class BrewController : ControllerBase {

		[HttpGet("{name}")]
		public IActionResult Get(string name) {
			if(!Data.HasBrew(name))
				return NotFound();
			var brew = Data.GetBrew(name);
			return new JsonResult(brew.Meta);
		}

	}

	[ApiController]
	[Route("misc")]
	public sealed class MiscController : ControllerBase {

		public IActionResult Get(string type, string name = null) {
//			if(!Misc.HasHandle(type))
				return NotFound();
		}

	}

}