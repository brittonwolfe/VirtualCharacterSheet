using System;
using System.Collections.Generic;
using System.IO;

using VirtualCharacterSheet.Event;
using VirtualCharacterSheet.Exceptions;
using VirtualCharacterSheet.IO.Serialization;

namespace VirtualCharacterSheet {

	public class HitDice {
		public ushort Die;
		public uint Count;

		public HitDice(ushort d) : this(1, d) {}
		public HitDice(uint n, ushort d) {
			Die = d;
			Count = n;
		}

	}

	public abstract class Character : ScriptedObject {
		public string Name;
		public byte Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;
		public short STR { get { return Core.Modifier(Strength); } }
		public short DEX { get { return Core.Modifier(Dexterity); } }
		public short CON { get { return Core.Modifier(Constitution); } }
		public short INT { get { return Core.Modifier(Intelligence); } }
		public short WIS { get { return Core.Modifier(Wisdom); } }
		public short CHA { get { return Core.Modifier(Charisma); } }

		internal static event InjectionEvent Injection;

		public Character() : base() { }

		protected void Inject() {
			if(Injection != null)
				try { Injection.Invoke(this); }
				catch(Exception e) { Console.WriteLine(e); }
		}

	}

	[IO.Serialization.Serializable(typeof(PlayerCharacter), "", "")]
	public class PlayerCharacter : Character {
		public string Player;
		public string Identifier { get { return (Player + ":" + Name); } }
		public bool Inspiration { get; private set; }

		private static Dictionary<string, (SerializationEvent, DeserializationEvent)> SerializerSets = new Dictionary<string, (SerializationEvent, DeserializationEvent)>();

		public PlayerCharacter(string name, string player) : base() {
			Name = name;
			Player = player;
			Data.SetCharacter(this);
			Inject();
		}

		public static void AddSerializationHook(string key, SerializationEvent serialize, DeserializationEvent deserialize) { SerializerSets[key] = (serialize, deserialize); }
		public static void RemoveSerializationHook(string key) { SerializerSets.Remove(key); }
		public static bool Serialize(PlayerCharacter pc, BinaryWriter writer, bool shouldclose = true) {
			Cellar.Serialize(Data.GetCellar(), writer, false);
			writer.Write(pc.Player);
			writer.Write(pc.Name);
			writer.Write(pc.Strength);
			writer.Write(pc.Dexterity);
			writer.Write(pc.Constitution);
			writer.Write(pc.Intelligence);
			writer.Write(pc.Wisdom);
			writer.Write(pc.Charisma);
			writer.Write(SerializerSets.Count);
			foreach(KeyValuePair<string, (SerializationEvent, DeserializationEvent)> kvp in SerializerSets) {
				writer.Write(kvp.Key);
				kvp.Value.Item1(pc, writer, false);
			}
			if(shouldclose)
				writer.Close();
			return true;
		}
		public static PlayerCharacter Deserialize(BinaryReader reader, bool shouldclose = true) {
			var cellar = Cellar.Deserialize(reader, false);
			if(cellar != Data.GetCellar())
				throw new CellarMismatchException(cellar, Data.GetCellar());
			string player = reader.ReadString();
			string name = reader.ReadString();
			var output = new PlayerCharacter(name, player);
			output.Strength = reader.ReadByte();
			output.Dexterity = reader.ReadByte();
			output.Constitution = reader.ReadByte();
			output.Intelligence = reader.ReadByte();
			output.Wisdom = reader.ReadByte();
			output.Charisma = reader.ReadByte();
			int serialcount = reader.ReadInt32();
			if(serialcount > SerializerSets.Count)
				throw new MissingSerializationHandlersException(serialcount, SerializerSets.Count);
			for(int x = 0; x < serialcount; x++) {
				string key = reader.ReadString();
				var tmp = (ScriptedObjectSet)SerializerSets[key].Item2(reader, false);
				foreach(string k in tmp.Meta.Keys)
					((IDictionary<string, object>)output.Meta)[k] = tmp.Meta[k];
				foreach(string k in tmp.Info.Keys)
					((IDictionary<string, object>)output.Info)[k] = tmp.Info[k];
			}
			if(shouldclose)
				reader.Close();
			return output;
		}

	}

	public class NPC : Character {
		public string Module;
		public string Identifier { get { return (Module + ":" + Name); } }

		public NPC(string name, string module) : base() {
			Name = name;
			Module = module;
			Inject();
		}

	}

	public class Feat : ScriptedObject {
		public string Name;
		public string Description;

		public Feat(string title) {
			Name = title;
			Data.SetFeat(Name, this);
		}

	}

	public class Class : ScriptedObject {
		public readonly string Identifier;
		public string Name;
		public ushort HitDie;
		public bool[] Saves;
		internal List<dynamic> OnLevel = new List<dynamic>();

		public Class(string n) {
			Identifier = n;
			if (Data.HasClass(n))
				throw new ClassAlreadyExistsException(Identifier);
			Data.SetClass(Identifier, this);
			Saves = new bool[6];
		}

		public void AttachInstance(Character c) {
			var tmp = new ClassInstance(this);
			c.Meta.classes.append(tmp);
		}

	}
	
	public class ClassInstance {
		public ushort Level { get; private set; }
		private Class underlying;
		private Character attached;

		internal ClassInstance(Class c) { underlying = c; }

		internal void Attach(Character character, ushort lvl = 1) {
			attached = character;
			Level = lvl;
		}

		public void LevelUp() { underlying.OnLevel[++Level](attached); }

		public override string ToString() { return ("Instance of " + underlying.ToString() + " attached to " + attached.ToString()); }

	}

}