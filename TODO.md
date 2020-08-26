# To-Do

## Refactors

- [ ] Refactor the different types of init methods in Scripting to use scopes. This means we can load a brew in one scope, run a sandbox in another, and run behaviors in yet another. This makes it *way* easier to put together the environment.

## UI Progress

- [ ] Create TUIs
- [ ] Make GUIs work

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

- [ ] Try using Rust!
- [ ] Add scripting support for Lua
- [ ] Add scripting support for Ruby
- [ ] Add support for [C# scripting through the JIT IL](https://docs.microsoft.com/en-us/dotnet/standard/managed-execution-process)???

