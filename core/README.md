# VCS Core Python Library

VCS comes with a core library to provide functionality to build brew packages. This includes helpers for things like custom shell-like TUIs, custom UIs, and so on.

## core.io

the `core.io` package contains helper functions for de/serialization.

### Functions

- `load_object(type, File) -> object` – loads an `object` from a file. The provided `type` must have a callable static member `Deserialize`.
- `save_object(object, File) -> bool` – saves an `object` to a file. The object's `type` must have a callable static member `Serialize`.

## core.shell

the `core.shell` package includes classes and methods to aid in the creation of shell-like interfaces and handling.

### Classes

- `PyTui` – Used to create a shell interface for the VCS environment.

### Functions

- `add_base(dict) -> dict` – a helper function that compiles a command `dict` from the given `dict` that includes the commands from the `basic_shell_dict`.
- `cmd_brew(list)` – a command for the user to interact with brews.
- `cmd_load(list)` – a command for the user to deserialize objects from files.
- `cmd_roll(list)` – a command for the user to "roll" the built-in die.
- `cmd_save(list)` – a command for the user to serialize objects to files.
- `cmd_view(list)` – a command for the user to view objects with scripted viewers.
- `shell(AbstractTui, **kwargs)` – starts a `PyTui` shell instance.

### Misc. Variables

- `basic_shell` – the base shell for VCS that is used for the built-in sandbox function.
- `basic_shell_dict` – the dict containing all the functions for `basic_shell`.

Used to create a shell interface for interacting with a subset of the VCS environment.

## core.ui

the `core.ui` package includes classes and methods to aid in the creation of user interfaces.

### Classes

- `PyCharacterSheet` – ...

