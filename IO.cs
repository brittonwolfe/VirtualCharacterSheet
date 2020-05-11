using System.IO;

namespace VirtualCharacterSheet {

	public static class FileLoad {
		public static readonly string TempPath = Path.GetTempPath();

		public static IO.File GetTempFile(string name) {
			return null;
		}

	}

	namespace IO {

		public class File {
			public string Path;

			public File(string p) { Path = p; }


		}



	}

}