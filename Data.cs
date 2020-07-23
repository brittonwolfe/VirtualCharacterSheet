using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using VirtualCharacterSheet.Event;
using VirtualCharacterSheet.Exceptions;
using VirtualCharacterSheet.IO;
using VirtualCharacterSheet.IO.Serialization;

namespace VirtualCharacterSheet {

	public static class Data {
		private static Dictionary<string, PlayerCharacter> character = new Dictionary<string, PlayerCharacter>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private static Dictionary<string, Feat> feat = new Dictionary<string, Feat>();
		private static Dictionary<string, Item> item = new Dictionary<string, Item>();
		private static Dictionary<string, NPC> npc = new Dictionary<string, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();
		private static Dictionary<string, object> pyf = new Dictionary<string, object>();

		private static Dictionary<string, Brew> brew = new Dictionary<string, Brew>();

		public static Item GetItem(string key) { return item[key.ToLower()]; }
		public static PlayerCharacter GetCharacter(string key) { return character[key.ToLower()]; }
		public static NPC GetNPC(string key) { return npc[key.ToLower()]; }
		public static RawPyScript GetPy(string key) { return py[key.ToLower()]; }
		public static object GetPyF(string key) { return pyf[key.ToLower()]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }
		public static Feat GetFeat(string key) { return feat[key.ToLower()]; }

		public static void SetItem(Item i) { item[i.Identifier.ToLower()] = i; }
		public static void SetCharacter(PlayerCharacter c) { character[c.Identifier.ToLower()] = c; }
		public static void SetNPC(string key, NPC n) { npc[key.ToLower()] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key.ToLower()] = src; }
		public static void SetPyF(string key, object func) { pyf[key.ToLower()] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }
		public static void SetFeat(string key, Feat f) { feat[key.ToLower()] = f; }

		public static bool HasItem(string key) { return item.ContainsKey(key.ToLower()); }
		public static bool HasCharacter(string key) { return character.ContainsKey(key.ToLower()); }
		public static bool HasNPC(string key) { return npc.ContainsKey(key.ToLower()); }
		public static bool HasPy(string key) { return py.ContainsKey(key.ToLower()); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key.ToLower()); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }
		public static bool HasFeat(string key) { return feat.ContainsKey(key.ToLower()); }

		internal static void AddBrew(Brew b) { brew[b.Name] = b; }
		public static bool HasBrew(string n) { return brew.ContainsKey(n); }
		public static Brew GetBrew(string n) { return brew[n]; }
		internal static List<Brew> GetAllBrews() { return new List<Brew>(brew.Values); }
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

	}

	public class Brew {
		public dynamic Meta = new ExpandoObject();
		public Dictionary<Type, Forms.AbstractUiFactory> Viewers;
		public readonly string Name;

		public Brew(string name) {
			Name = name;
			if (Data.HasBrew(name))
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
				Console.WriteLine("No viewer for " + T + " was found.");
				return;
			}
			Viewers[T].Create(obj).Render();
		}
		public bool CanView(Type T) { return Viewers.ContainsKey(T); }

		public File GetFile(string subpath) { return new File(Meta.Dir + subpath); }

		public static void Load(File src) { Scripting.Brew(new FileScript(src)); }

	}

	public class Item : ScriptedObject {
		public string Name;
		public readonly string Identifier;

		public Item(string id) : base() {
			Identifier = id;
			Data.SetItem(this);
		}

	}

}
