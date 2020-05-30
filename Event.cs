using System.Collections.Generic;
using System.Dynamic;

namespace VirtualCharacterSheet.Event {

	public delegate short CheckEvent(byte ability);
	public delegate short SkillCheckEvent(string skill);
	public delegate void InjectionEvent(Character sender);

	public class DynamicBehaviorSet : DynamicObject {
		private Dictionary<string, DynamicBehavior> Behaviors;
		internal readonly object Source;

		public DynamicBehaviorSet(object src) {
			Source = src;
			Behaviors = new Dictionary<string, DynamicBehavior>();
		}

		public bool Contains(string key) { return Behaviors.ContainsKey(key); }

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

	internal class DynamicBehavior : DynamicObject {
		private DynamicBehaviorSet Parent;
		private readonly dynamic Source;

		internal DynamicBehavior(DynamicBehaviorSet set, dynamic func) {
			Source = func;
			Parent = set;
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
			if(args.Length == 0)
				result = Source(Parent.Source);
			else
				result = Source(Parent.Source, args);
			return true;
		}

	}

}