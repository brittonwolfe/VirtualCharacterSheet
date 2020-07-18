# VCS Shell

A nice feature built in to VCS is running a shell, whether that's to sandbox the scripting environment, to add a debug mode to your GUIs, or to add more expansive functionality to your TUIs, you can use a shell! The default shell, `core.shell.basic_shell`, is loaded with some basic features, but you can build upon it by either picking directly which commands you'd like, or take them all with `core.shell.add_base(dict)`.

You can run a shell in a loop through `core.shell.shell()`; by default, it uses the basic shell, but you can run any shell by using the `tui` keyword argument.

Creating your own shell is also easy! Just use `core.shell.PyTui` as a base! It has a built-in help function that prints a function's docstring, and is customizable to allow you to decide if you want your TUI to automatically print returned values, and if you want to allow the user to use a colon (`:`) as an escape character to directly run Python code.