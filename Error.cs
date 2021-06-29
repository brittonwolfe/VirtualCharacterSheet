using System;
using VCS.IO.Serialization;

namespace VCS.Exceptions {

	public class OccupiedIdentityException : Exception {
		public readonly Type Type;
		public readonly string Identifier;

		public OccupiedIdentityException(string id)
		: base($"An object with the identifier \"{id}\" already exists.") {
			Identifier = id;
			Type = typeof(object);
		}
		public OccupiedIdentityException(string id, Type t)
		: base($"A(n) {t} with the identifier \"{id}\" already exists.") {
			Identifier = id;
			Type = t;
		}

	}

	public class ClassAlreadyExistsException : Exception {
		public readonly string ClassName;

		public ClassAlreadyExistsException(string name)
		: base($"A character class with the name \"{name}\" already exists!") {
			ClassName = name;
		}

	}

	public class BrewKeyOccupiedException : Exception {
		public readonly Brew Brew;

		public BrewKeyOccupiedException(Brew b)
		: base($"A brew with the name \"{b.Name}\" already exists!") {
			Brew = b;
		}
	}

	public sealed class CellarMismatchException : Exception {
		public readonly Cellar Expected;
		public readonly Cellar Present;

		public CellarMismatchException(Cellar expected, Cellar current)
		: base($"Expected {expected.ToString()} to load object, but got {current.ToString()}") {
			Expected = expected;
			Present = current;
		}
	}

	public sealed class MissingSerializationHandlersException : Exception {
		public readonly int Expected;
		public readonly int Present;

		public MissingSerializationHandlersException(int expected, int got)
		: base($"Expected to get {expected} serialization handlers, but only {got} were present (missing {(expected - got)})") {
			Expected = expected;
			Present = got;
		}

	}

}