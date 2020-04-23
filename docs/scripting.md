# Python Scripting
VCS is scripted with IronPython.

## Global Functions
### roll(d)
`roll(d)` is used to roll a single `d`-sided die. It accepts any .NET `ushort` for the number of sides, and returns a `ushort`.

### rolln(n,d)
`rolln(n,d)` rolls `n` `d`-sided dice. It accepts a `byte` for the number, `n`, of dice, and, like the `roll(d)` method, it accepts a `ushort` for the number of sides, and returns a `ushort`.

### mod(s)
`mod(s)` returns the modifier of a score `s`. It accepts a score, `s `, as a `byte` and returns a `short` back.

### getopenchar()
`getopenchar()` returns the `Character` object for the currently loaded by the character sheet viewer, or `null` if there is no character currently open.
