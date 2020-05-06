# Python Scripting
VCS is scripted with IronPython.

## Global Accessors

### `_c(id)`

returns the character with the given `id`.

### `_i(id)`

returns the item with the given `id`.

### `_n(id)`

returns the NPC with the given `id`.

### `_s(id)`

returns the spell with the given `id`.

## Accessing Local Variables

Accessing locals is done entirely through the `local` variable. It's a dynamic object, so its values are assignable at runtime.

### `arg`

`local.arg` is the argument sent to the script, if there is one.

## Global Functions

### `getopenchar()`

`getopenchar()` returns the `Character` object for the currently loaded by the character sheet viewer, or `null` if there is no character currently open.

### `mod(s)`

`mod(s)` returns the modifier of a score `s`. It accepts a score, `s `, as a `byte` and returns a `short` back.

### `roll(d)`
`roll(d)` is used to roll a single `d`-sided die. It accepts any .NET `ushort` for the number of sides, and returns a `ushort`.

### `rolln(n,d)`
`rolln(n,d)` rolls `n` `d`-sided dice. It accepts a `byte` for the number, `n`, of dice, and, like the `roll(d)` method, it accepts a `ushort` for the number of sides, and returns a `ushort`.

## Metascripting Tools

Metascripting is a weird idea, but I wanted to at least include an explanation: the scripting engine has the capability to add or change scripted behaviors at runtime through a file or direct input through the console. This basically means that you can script stuff through the console without needing any external editor.

### Naming Conventions

I'd like to at least try to have some rules and best practices put forward for consistency and sanity; if you are defining scripts during runtime through a module, please use the naming structure `"package:name"` to avoid conflicts at runtime.

### `_py(name)`

Returns the runtime-defined Python script with the given name. 

###`new_py(name)`

Opens the command line script editor. It can be exited by entering two blank lines.