

using System.Collections.Generic;

namespace VirtualCharacterSheet.Event {

	public delegate short CheckEvent(byte ability);
	public delegate short SkillCheckEvent(string skill);
	public delegate void InjectionEvent(Character sender);

	public class DynamicEventCollection {
		private Dictionary<string, List<Script>> Behaviors;
	}

}