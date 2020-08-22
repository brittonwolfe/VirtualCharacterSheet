using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace VirtualCharacterSheet.Event {

	public delegate short CheckEvent(byte ability);
	public delegate short SkillCheckEvent(string skill);
	public delegate void InjectionEvent(Character sender);
	public delegate bool SerializationEvent(object target, BinaryWriter writer, bool shouldclose = true);
	public delegate object DeserializationEvent(BinaryReader reader, bool shouldclose = true);

	public class DynamicBehaviorSet : DynamicObject {
		private Dictionary<string, DynamicBehavior> Behaviors;
		internal readonly object Source;

		internal DynamicBehavior this[string key] {
			get { return Behaviors[key]; }
			set { Behaviors[key] = value; }
		}

		public DynamicBehaviorSet(object src) {
			Source = src;
			Behaviors = new Dictionary<string, DynamicBehavior>();
		}

		public bool Contains(string key) { return Behaviors.ContainsKey(key); }

		internal dynamic Do(string name) { return Behaviors[name].Do(); }

		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			if(!Contains(binder.Name)) {
				result = null;
				return false;
			}
			result = Behaviors[binder.Name];
			return true;
		}
		public override bool TrySetMember(SetMemberBinder binder, object value) {
			Add(binder.Name, value);
			return true;
		}

		public void Add(string key, dynamic obj) {
			if(obj.GetType() == typeof(DynamicBehavior))
				Behaviors.Add(key, (DynamicBehavior)obj);
			else
				Behaviors.Add(key, new DynamicBehavior(this, obj));
		}

	}

	public class DynamicBehavior : DynamicObject {
		protected DynamicBehaviorSet Parent;
		protected readonly dynamic Source;

		internal protected DynamicBehavior(DynamicBehaviorSet set, dynamic func) {
			Source = func;
			Parent = set;
		}

		internal dynamic Do() { return ((dynamic)this)(); }

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
			if(args.Length == 0)
				result = Source(Parent.Source);
			else
				result = Source(Parent.Source, args);
			return true;
		}

		public dynamic __call__() { return Do(); }

	}

}