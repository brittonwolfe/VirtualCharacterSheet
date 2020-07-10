using System.IO;
using System.Reflection;

namespace VirtualCharacterSheet {

	public static class FileLoad {
		public static readonly string TempPath = Path.GetTempPath();
		public static IO.File GetTempFile(string name) { return new IO.File(TempPath + name); }
		public static IO.File GetInternalFile(string name) { return new IO.File(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + name); }

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

			public bool Exists() { return System.IO.File.Exists(this.Path); }

			internal BinaryWriter GetBinaryWriter() { return new BinaryWriter(System.IO.File.OpenWrite(this.Path)); }

		}

		public class Dir {
			public string Path;

			public Dir(string path) {
				Path = path;
			}

			public Dir GetSubdir(string sub) { return new Dir(Vpath() + sub); }
			public File Get(string sub) { return new File(Vpath() + sub); }

			private string Vpath() {
				if(!Path.EndsWith('\\'))
					return (Path + "\\");
				else
					return Path;
			}

		}

	}

}