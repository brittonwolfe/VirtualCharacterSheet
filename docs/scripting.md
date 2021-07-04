# Python Scripting

This documentation is still incomplete and is subject to addition.

VCS uses IronPython to embed Python. It provides a wide and redundant library to welcome developers of all kinds.

### Casting functions

`byte` and `short` are implemented in the same way other Python cast functions are, since Python does not natively support these primitives. Just like other conversions, it's as simple as `byte(obj)` or `short(obj)`.

### The config object

`__config__` makes the configuration object visible so that brew developers can easily check the user's preferences and add/remove their own. It's a wrapped `ConfigParser` object with a bit more utility. You can find it in `core.config` if you're curious about its functionality.

## The Utils class

The `VCS.Util` class provides helper functions and some helper objects.

### Objects

- `Util.local` exposes the CLR's `Scripting.locals` object to the Python environment. This exists so that developers have options for how they design their functions and brew environment.

- `Util.brew` exposes the `Scripting.homebrew` object that provides helper functions for brew development. For brew environments (`brew.py` or any files imported by a `brew.py`), this exposes the following:

  - `brew.Path`: the directory containing the currently initializing `brew.py` file.
  - `brew.def_brew`: a helper method that creates a new `Brew` object.

  Otherwise, it exposes the `brew.load(string path)` function, which loads a brew.

- `_setting` is for temporary settings, like whether or not the current shell should print its output.

### Methods

#### General

- `readl(str prompt)`: the `input(str prompt)` global doesn't like IronPython, so use `readl` instead.
- `roll(ushort sides)`: rolls a `sides`-sided die.
- `rolln(ushort sides, ushort count)`: rolls an `sides`-sided die `count` times.
- `view(object obj, Brew brew = null)`: views an object. If the `brew` parameter is specified, it will use the viewer from that `Brew`.

#### Accessors

- `_brew(str name)`: gets the `Brew` object with the given `name`.
- `_c(str name)`: gets the `PlayerCharacter` object with the given `name`.
- `_class(str name)`: gets the `Class` object with the given `name`.
- `_i(str name)`: gets the `Item` object with the given `name`.
- `_n(str name)`: gets the `NPC` object with the given `name`.
- `_py(str name)`: gets the `RawPyScript` object with the given `name`. You probably don't need to use this.
- `_pyf(str name)`: gets a Python function with the given name. You probably don't need to use this.

#### Checkers

- `has_brew(str name)`: returns a `bool` indicating if a `Brew` with the given `name` already exists.
- `has_c(str name)`: returns a `bool` indicating if a `PlayerCharacter` with the given `name` already exists.
- `has_class(str name)`: returns a `bool` indicating if a `Class` with the given `name` already exists.
- `has_feat(str name)`: returns a `bool` indicating if a `Feat` with the given `name` already exists.
- `has_i(str name)`: returns a `bool` indicating if an `Item` with the given `name` already exists.
- `has_n(str name)`: returns a `bool` indicating if an `NPC` with the given `name` already exists.
- `has_py(str name)`: returns a `bool` indicating if a `RawPyScript` with the given `name` already exists.
- `has_pyf(str name)`: returns a `bool` indicating if a Python function with the given `name` already exists.

#### Initializers

- `def_c(str name, str player)`: returns a new `PlayerCharacter` with the given `name` and `player` name.
- `def_class(str name)`: returns a new `Class` with the given `name`.
- `def_feat(str name)`: returns a new `Feat` with the given `name`.
- `def_i(str name)`: returns a new `Item` with the given `name`.

#### Metafunction

If you find yourself needing (probably for magic) to manipulate function behaviors, then the following functions exist:

- `set_pyf(str name, function func)`: stores a Python function with the given `name`.
- `set_py(str name, RawPyScript script)`: stores a `RawPyScript` with the given `name`.

## The Data class

`VCS.Data` also exposes some useful functions and indexers.

### Indexers

The indexer surrogates exist to give developers another option more resembling of the way the CLI resolves object references. `_brew`, `_C`, `_class`, `_feat`, `_i`, `_n`, `_py`, and `_pyf` are currently implemented. These objects also provide a `bool has(str id)` method to check if a name exists (this method also exists as `bool Has(string id)`, but both are included for ease of use between different languages).

### Methods

For all intents and purposes, I'm excluding any methods that don't already have equivalents elsewhere (such as the `__config__` object or `Util` class) which abide by Python naming conventions.

- `AllCharacters()`: returns a new-line separated string listing all `PlayerCharacter` IDs. Meant to be human-readable. Less useful than `GetAllCharacters`.
- `AllBrews()`: returns a space-separated string listing all `Brew` names. Meant to be human-readable. Less useful than `GetAllBrews`.
- `GetAllCharacters()`: returns a `List` of all `PlayerCharacter` objects. More useful than `AllCharacters`.
- `GetAllBrews()`: returns a `List` of all `Brew` objects. More useful than `AllBrews`.
- `GetCellar()`: gets a `Cellar` object for serialization. This likely isn't particularly useful, but it *does* exist.