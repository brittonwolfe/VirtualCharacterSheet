# VCS Shell

VCS has a module for running a CLI for the user to interact with the environment, whether that's to sandbox the scripting environment, to add a debug mode to your GUIs, or to add more expansive functionality to your TUIs, you can use a shell! The default shell, `core.shell.basic_shell`, is loaded with some basic features, but you can build upon it by either picking directly which commands you'd like, or take them all with `core.shell.add_base(dict)`.

```python
from core.shell import PyTui, add_base, basic_shell, shell

def sample_command(args, **kwargs):
    return 'foo'

some_shell = PyTui(
    add_base({
        'sayfoo': sample_command
    }),
    shout = True, #whether or not to echo returns,
    colon = True, #whether or not to use colon syntax
    name = 'Some Shell' #used for the which command
)
```

You can also prune from the default commands by passing the `prune` keyword argument to `add_base(dict, bool)`:

```python
add_base({'''your commands'''}, prune = ['''commands to removed'''])
```

## shout and colon

The `shout` and `colon` arguments provide a little more control and versatility. Setting `shout` to `True` means any value returned by a function will be echoed, which can provide more clarity to the user. Setting `colon` to `True` enables "colon syntax," where the user can start a line with a colon to execute a Python statement.

## Running your shell

`core.shell.non_loop_shell(tui, **kwargs)` and `core.shell.shell(tui, **kwargs)` both allow you to create a REPL for your shell. `non_loop_shell` will perform a single statement, while `shell` will loop until the shell gets the breaking `exit` command.

```python
# assuming some_shell
shell(some_shell)
```

The non-looping shell is more suited for TUIs that have command inputs.

Also, both functions accept keyword arguments that will be passed to all the commands to allow contexts.

```python
shell(some_shell, param = object)
```