# To-Do

## Refactors

- [ ] Refactor init to just set up important things, and move helper methods to Scripting.Utils class
	- [ ] create the class for the homebrew helper object
- [ ] Make `help` (or `help shell`?) list all commands

## UI Progress

- [ ] Create TUIs
- [x] Make GUIs work
- [ ] Make GUIs

### TUI features

- [ ] Implement a way to make TUI templates similar to Django's
- [ ] Make TUIs live-update
- [ ] Make TUIs have responsive design to reflect the space they have in the terminal?

## Serialization

- [x] Make the character serializer
- [x] Make the 5e module's character serializer

## NI/O

- [x] Write a host for networking and communicating game state, preferably RESTful.
- [ ] Finish writing other API controllers
- [ ] Write a client for connecting to the host to synchronize and communicate the game state.

## Experimental

- [ ] Add support for Glade# for another brew UI option
- [ ] Add scripting support for Lua
- [ ] Add scripting support for Ruby
- [ ] Add support for [C# scripting through the JIT IL](https://docs.microsoft.com/en-us/dotnet/standard/managed-execution-process)???

