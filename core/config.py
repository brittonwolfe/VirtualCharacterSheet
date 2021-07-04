
from configparser import ConfigParser
from os.path import expanduser

class Config():
	"""	Configuration File object.
		The class itself is a high-level wrapper for
		`ConfigParser` with methods for IO.
		See Also
		--------
		__config__ : Application config object.
	"""
	values = None
	parser = None
	def __init__(self, path = '~/.config/vcs/config'):
		self.path = expanduser(path)
		self.parser = ConfigParser()
		self.parser.read(self.path)
		self.values = {}
		for section in self.parser.sections():
			obj = {}
			for kvp in self.parser.items(section):
				obj[kvp[0]] = kvp[1]
			self.values[section] = obj
	def has_section(self, name):
		return name in self.values
	def has_option(self, section, name):
		return name in self.values[section]
	def save(self):
		for section in self.values:
			for key in self.values[section]:
				self.parser.set(section, key, str(self.values[section][key]))
		self.parser.write(fp = self.path)
	def __getitem__(self, key):
		return self.values[key]

__config__ = Config()
