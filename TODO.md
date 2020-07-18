# To-Do

- Refactor definitions; I want to move things around to better align with the new AbstractUi-style handling, where the classes themselves are created in the brews. This way, viewers can be less complex, and it's easier to hunt down what type(s) are causing errors with a larger set of brews.
- Refactor the different types of init methods in Scripting to use scopes. This means we can load a brew in one scope, run a sandbox in another, and run behaviors in yet another. This makes it *way* easier to put together the environment.
- Create TUIs and GUIs
- Serialization