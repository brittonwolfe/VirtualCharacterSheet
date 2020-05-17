using System;
using System.IO;

namespace VirtualCharacterSheet {

	public static class FileLoad {
		public static readonly string TempPath = Path.GetTempPath();
		public static IO.File GetTempFile(string name) { return new IO.File(TempPath + name); }

		public static Stream GetStream(IO.File file) { return new FileStream(file.Path, FileMode.OpenOrCreate, FileAccess.ReadWrite); }

	}

	namespace IO {

		public class File {
			public string Path;
			public Dir Directory {
				get;
				private set;
			}

			public File(string p) {
				Path = p;
				Directory = new Dir(new FileInfo(Path).Directory.ToString());
			}

			public string ReadText() {
				string output = "";
				string line;
				using(StreamReader src = new StreamReader(FileLoad.GetStream(this)))
					while((line = src.ReadLine()) != null)
						output += line + "\n";
				return output.Trim();
			}
			public void WriteText(string t) {
				using(StreamWriter output = new StreamWriter(FileLoad.GetStream(this)))
					output.Write(t);
			}

		}

		public class Dir {
			public string Path;

			public Dir(string path) {
				Path = path;
			}

			//public File FromDir(string sub) { }

		}

	}

}