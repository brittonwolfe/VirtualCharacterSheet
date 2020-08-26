# Using configurations

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

In the event that it's necessary to create another config option--for organization, I guess? there's also the option to create a new `core.config.Config` object with the `path` argument set to your filename.