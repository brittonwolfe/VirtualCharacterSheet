clr.AddReference('vcs')
from configparser import ConfigParser
from os.path import expanduser

class Config():
	values = {}
	parser = None
	def __init__(self, path = '~/.config/vcs/config'):
		self.parser = ConfigParser()
		self.parser.read(expanduser(path))
		for section in self.parser.sections():
			obj = {}
			for name in self.parser.items(section)[0]:
				obj[name] = self.parser.get(section, name)
			self.values[section] = obj
	def has_section(self, name):
		return hasattr(self.values, name)
	def save(self):
		for section in self.values:
			for key in self.values[section]:
				self.parser.set(section, key, str(self.values[section][key]))
		self.parser.write()
	def __getitem__(self, key):
		return self.values[key]

global __config__
__config__ = Config()
