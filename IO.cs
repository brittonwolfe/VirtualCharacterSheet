using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VirtualCharacterSheet {

	public static class FileLoad {
		public static readonly string TempPath = Path.GetTempPath();
		public static IO.File GetTempFile(string name) { return new IO.File(TempPath + name); }
		public static IO.File GetInternalFile(string name) { return new IO.File(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + name); }

		public static IO.Dir WorkingDirectory() { return new IO.Dir(System.IO.Directory.GetCurrentDirectory()); }

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
			internal BinaryReader GetBinaryReader() { return new BinaryReader(System.IO.File.OpenRead(this.Path)); }

		}

		public class Dir {
			public string Path;

			public Dir(string path) {
				Path = path;
			}

			public Dir GetSubdir(string sub) { return new Dir(Vpath() + sub); }
			public File Get(string sub) { return new File(Vpath() + sub); }

			public bool Exists() { return System.IO.Directory.Exists(this.Path); }

			private string Vpath() {
				if(!Path.EndsWith('/'))
					return (Path + "/");
				else
					return Path;
			}

		}

		namespace Serialization {

			internal sealed class Bottle : ISerializable {
				public string[] MetaImage;
				public string BrewName;

				internal Bottle(BinaryReader reader) { ((ISerializable)this).Deserialize(reader); }
				public Bottle(File file) { ((ISerializable)this).Deserialize(file); }
				public Bottle(Brew b) {
					BrewName = b.Name;
					var metakeys = ((IDictionary<string, object>)b.Meta).Keys;
					MetaImage = new string[metakeys.Count];
					int n = 0;
					foreach(string key in metakeys)
						MetaImage[n++] = key;
				}

				bool ISerializable.Serialize(BinaryWriter writer) {
					writer.Write(BrewName);
					writer.Write(MetaImage.Length);
					foreach(string propname in MetaImage)
						writer.Write(propname);
					return true;
				}
				bool ISerializable.Deserialize(BinaryReader reader) {
					BrewName = reader.ReadString();
					int length = reader.ReadInt32();
					MetaImage = new string[length];
					for(int x = 0; x < length; x++)
						MetaImage[x] = reader.ReadString();
					return true;
				}

				public static bool operator ==(Bottle a, Bottle b) {
					if(a.BrewName != b.BrewName)
						return false;
					if(a.MetaImage.Length != b.MetaImage.Length)
						return false;
					foreach(string propA in a.MetaImage) {
						bool found = false;
						foreach(string propB in b.MetaImage)
							if(found)
								break;
							else if(propA == propB)
								found = true;
						if(!found)
							return false;
					}
					return true;
				}
				public static bool operator ==(Bottle a, Brew b) { return a == new Bottle(b); }
				public static bool operator !=(Bottle a, Bottle b) { return !(a == b); }
				public static bool operator !=(Bottle a, Brew b) { return !(a == b); }

				public override bool Equals(object b) {
					var type = b.GetType();
					if(type != typeof(Bottle) && type != typeof(Brew))
						return false;
					if(type == typeof(Bottle))
						return this == (Bottle)b;
					else
						return this == (Brew)b;
				}
				public override int GetHashCode() { return base.GetHashCode(); }

			}

			public sealed class Cellar : ISerializable {
				internal Bottle[] bottles;

				public Cellar(File file) {  ((ISerializable)this).Deserialize(file); }

				bool ISerializable.Serialize(BinaryWriter writer) {
					writer.Write(bottles.Length);
					foreach(Bottle bottle in bottles)
						((ISerializable)bottle).Serialize(writer);
					return true;
				}
				bool ISerializable.Deserialize(BinaryReader reader) {
					bottles = new Bottle[reader.ReadInt32()];
					for(int x = 0; x < bottles.Length; x++)
						bottles[x] = new Bottle(reader);
					return true;
				}

				public bool Contains(Brew brew) {
					foreach(Bottle bottle in bottles)
						if(bottle == brew)
							return true;
					return false;
				}
				internal bool Contains(Bottle bottle) {
					foreach(Bottle sourcebottle in bottles)
						if(sourcebottle == bottle)
							return true;
					return false;
				}

			}

			public interface ISerializable {

				public sealed bool Serialize(File file) {
					return Serialize(file.GetBinaryWriter());
				}
				public sealed bool Deserialize(File file) {
					return Deserialize(file.GetBinaryReader());
				}
				public abstract bool Serialize(BinaryWriter writer);
				public abstract bool Deserialize(BinaryReader reader);

			}

			

		}

	}

}