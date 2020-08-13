clr.AddReference('vcs')
from configparser import ConfigParser
from os.path import expanduser

config = ConfigParser()
config.read(expanduser('~/.config/vcs.conf'))

global __config__
__config__ = {}

for section in config.sections():
	obj = {}
	for name in config.items(section)[0]:
		obj[name] = config.get(section, name)
	__config__[section] = obj

def save_config():
	for section in __config__:
		for key in __config__[section]:
			config.set(section, key, str(__config__[section][key]))
	config.write()
