using System;

namespace VirtualCharacterSheet.Exceptions {

	public class ClassAlreadyExistsException : Exception {
		public readonly string ClassName;

		public ClassAlreadyExistsException(string name)
		: base("A class with the name \"" + name + "\" already exists!") {
			ClassName = name;
		}

	}

}