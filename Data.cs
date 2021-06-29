using System;
using System.Collections.Generic;
using System.Dynamic;

using VCS.Event;
using VCS.Exceptions;
using VCS.IO;
using VCS.IO.Serialization;

using BinaryWriter = System.IO.BinaryWriter;
using PyFunc = IronPython.Runtime.PythonFunction;

namespace VCS {

	public readonly struct IndexSurrogate<T> {
		private readonly Dictionary<string, T> source;

		internal IndexSurrogate(ref Dictionary<string, T> dict) { source = dict; }

		public T this[string str] {
			get => source[str];
		}		

		public bool Has(string id) { return source.ContainsKey(id); }
		public bool has(string id) { return Has(id); }

	}

	public static class Data {
		internal static dynamic Config = null;

# region dictionaries
		private static Dictionary<string, PlayerCharacter> character = new Dictionary<string, PlayerCharacter>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private static Dictionary<string, Feat> feat = new Dictionary<string, Feat>();
		private static Dictionary<string, Item> item = new Dictionary<string, Item>();
		private static Dictionary<string, NPC> npc = new Dictionary<string, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();
		private static Dictionary<string, PyFunc> pyf = new Dictionary<string, PyFunc>();

		private static Dictionary<string, Brew> brew = new Dictionary<string, Brew>();
# endregion

# region indexers
		public static readonly IndexSurrogate<Brew> _brew = new IndexSurrogate<Brew>(ref brew);
		public static readonly IndexSurrogate<PlayerCharacter> _C = new IndexSurrogate<PlayerCharacter>(ref character);
		public static readonly IndexSurrogate<Class> _class;
		public static readonly IndexSurrogate<Feat> _feat;
		public static readonly IndexSurrogate<Item> _i = new IndexSurrogate<Item>(ref item);
		public static readonly IndexSurrogate<NPC> _n;
		public static readonly IndexSurrogate<RawPyScript> _py = new IndexSurrogate<RawPyScript>(ref py);
		public static readonly IndexSurrogate<PyFunc> _pyf = new IndexSurrogate<PyFunc>(ref pyf);
# endregion

		public static event BrewLoadEventHandler BrewLoadEvent;

# region config
		public static dynamic GetConfig(string section, string option = null) {
			if(!ConfigHasSection(section))
				return null;
			if(option == null)
				return Config[section];
			if(!Config.has_option(section, option))
				return null;
			return Config[section][option];
		}
		public static bool ConfigHasSection(string section) { return Config.has_section(section); }
		public static void SaveConfig() { Config.save(); }
# endregion

# region getters
		public static Item GetItem(string key) { return item[key.ToLower()]; }
		public static PlayerCharacter GetCharacter(string key) { return character[key.ToLower()]; }
		public static NPC GetNPC(string key) { return npc[key.ToLower()]; }
		public static RawPyScript GetPy(string key) { return py[key.ToLower()]; }
		public static PyFunc GetPyF(string key) { return pyf[key.ToLower()]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }
		public static Feat GetFeat(string key) { return feat[key.ToLower()]; }
# endregion

# region setters
		public static void SetItem(Item i) { item[i.Identifier.ToLower()] = i; }
		public static void SetCharacter(PlayerCharacter c) { character[c.Identifier.ToLower()] = c; }
		public static void SetNPC(string key, NPC n) { npc[key.ToLower()] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key.ToLower()] = src; }
		public static void SetPyF(string key, PyFunc func) { pyf[key.ToLower()] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }
		public static void SetFeat(string key, Feat f) { feat[key.ToLower()] = f; }
# endregion

# region checkers
		public static bool HasItem(string key) { return item.ContainsKey(key.ToLower()); }
		public static bool HasCharacter(string key) { return character.ContainsKey(key.ToLower()); }
		public static bool HasNPC(string key) { return npc.ContainsKey(key.ToLower()); }
		public static bool HasPy(string key) { return py.ContainsKey(key.ToLower()); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key.ToLower()); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }
		public static bool HasFeat(string key) { return feat.ContainsKey(key.ToLower()); }
# endregion

# region playercharacter functions
		public static List<PlayerCharacter> GetAllCharacters() {
			var output = new List<PlayerCharacter>();
			foreach(var value in character.Values)
				output.Add(value);
			return output;
		}

		public static string AllCharacters() {
			if(character.Count == 0)
				return null;
			string output = "";
			foreach(string key in character.Keys)
				output += $"-C[{key}]\n";
			return output.Trim();
		}
# endregion

# region brew functions
		internal static void AddBrew(Brew b) {
			brew[b.Name] = b;
			if(BrewLoadEvent != null)
				BrewLoadEvent.Invoke(b);
		}
		public static bool HasBrew(string n) { return brew.ContainsKey(n); }
		public static Brew GetBrew(string n) { return brew[n]; }
		public static List<Brew> GetAllBrews() { return new List<Brew>(brew.Values); }
		public static string AllBrews() {
			string output = "";
			foreach(string key in brew.Keys)
				output += (key + " ");
			return output.Trim();
		}

		public static Cellar GetCellar() {
			var bottles = new Bottle[brew.Count];
			int n = 0;
			foreach(KeyValuePair<string, Brew> brewkvp in brew)
				bottles[n++] = new Bottle(brewkvp.Value);
			return new Cellar(bottles);
		}
# endregion

	}

	public class Brew {
		public dynamic Meta = new ExpandoObject();
		public Dictionary<Type, Forms.AbstractUiFactory> Viewers;
		public readonly string Name;

		public Brew(string name) {
			Name = name;
			if(Data.HasBrew(name))
				throw new BrewKeyOccupiedException(this);
			Viewers = new Dictionary<Type, Forms.AbstractUiFactory>();
			Data.AddBrew(this);
		}

		public void AddCharacterInjector(InjectionEvent e) { Character.Injection += e; }

		public void AddView(Type T, Forms.AbstractUiFactory ui) {
			if(Viewers.ContainsKey(T)) {
				Console.WriteLine("Can't set more than one viewer for a type!");
				return;
			}
			Viewers[T] = ui;
		}

		public void View(object obj) {
			var T = obj.GetType();
			if(!Viewers.ContainsKey(T)) {
				Console.WriteLine($"No viewer for {T} was found.");
				return;
			}
			Viewers[T].Create(obj).Render();
		}
		public bool CanView(Type T) { return Viewers.ContainsKey(T); }

		public File GetFile(string subpath) { return new File(Meta.Dir + subpath); }

		public static void Load(File src) { Scripting.Brew(new FileScript(src)); }

	}

	public class MiscObject {
		private Dictionary<string, dynamic> Props;

		public MiscObject() { Props = new Dictionary<string, dynamic>(); }
		public MiscObject(IDictionary<string, object> dict) {
			foreach(KeyValuePair<string, object> pair in dict)
				Props[pair.Key] = pair.Value;
		}

		public static bool Serialize(object target, BinaryWriter writer, bool shouldclose = true) {
			if(target.GetType() != typeof(MiscObject))
				return false;
			((dynamic)target).__serialize__();
			return true;
		}

	}

}
