# Python Scripting

This documentation is still incomplete and is subject to addition.

VCS is scripted with IronPython. It provides a lot of helper functions and objects to give developers options and tools for creating brews.

## Engine Globals

There are only a few global functions and objects that VCS puts into its Python engine.

### Casting functions

`byte` and `short` are implemented in the same way other Python cast functions are, since Python does not natively support these primitives. Just like other conversions, it's as simple as `byte(obj)` or `short(obj)`.

### The config object

`__config__` makes the configuration object visible so that brew developers can easily check the user's preferences and add/remove their own. It's a wrapped `ConfigParser` object with a bit more utility. You can find it in `core.config` if you're curious about its functionality.

## The Utils class

The `VirtualCharacterSheet.Util` class provides helper functions and some helper objects.

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
- /TODO: the rest/

## The Data class

`VirtualCharacterSheet.Data` also exposes some useful functions and indexers.