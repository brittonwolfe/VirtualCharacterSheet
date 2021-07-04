# Using configurations

## For the User

The config and data folder is found at `~/.config/vcs/`. The default configuration file is `config`, but brews are allowed to use their own configuration files to avoid confusion or conflicts, but this is optional. VCS has some configuration options you can change in the default file:

```ini
[main]
delete_temp = true
editor = atom
prefer_cli = false
save_open_c = true
```

- `delete_temp` – Whether or not to delete temporary script files; defaults to true.
- `editor` – a string that sets the editor command. Change this to the executable for your favorite editor.
- `prefer_cli` – Whether or not to use the text-based CLI rather than GUIs by default; defaults to false.
- `save_open_c` – Whether or not to prompt you to save open characters when you exit the application; defaults to true.

The main configuration state is stored in the global `__config__`, which has a few useful features, including checking if sections or options are present, and saving the config to the source file.

Adding to the config just consists of adding your config values to its `values` dictionary:

```python
mydict = {'foo': 'bar'}
global __config__
__config__.values['test'] = mydict
```

This sample code will add a `[test]` section to the config file--in this case, the default `~/.config/vcs/config` file:

```ini
[test]
foo = bar
```

It is notable that all values *are* stored and loaded as strings, so parse them accordingly!

```python
should_foo = bool(__config__['test']['foo'])
if should_foo:
    print('foo')
```

In the event that it's necessary to create another config option––for organization or avoiding configuration conflicts––the `core.config.Config` object constructor accepts a `path` argument that will direct it to another file.