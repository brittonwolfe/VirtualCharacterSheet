using System;

namespace VirtualCharacterSheet.Exceptions {

	public class ClassAlreadyExistsException : Exception {
		public readonly string ClassName;

		public ClassAlreadyExistsException(string name)
		: base("A character class with the name \"" + name + "\" already exists!") {
			ClassName = name;
		}

	}

	public class BrewKeyOccupiedException : Exception {
		public readonly Brew Brew;

		public BrewKeyOccupiedException(Brew b)
		: base("A brew with the name \"" + b.Name + "\" already exists!") {
			Brew = b;
		}
	}

	public class BrewConfigurationMismatchException : Exception {
	}

}