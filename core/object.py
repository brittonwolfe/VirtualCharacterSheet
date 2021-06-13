import VirtualCharacterSheet

from core.util import dynobj

class dyn():
	def __init__(self, obj):
		object.__setattr__(self, "inner", obj)
	def __getattr__(self, name):
		output = None
		try:
			output = object.__getattribute__(self, name)
		except:
			output = self.inner.__getattr__(name)
		return output
	def __setattr__(self, name, value):
		self.inner.__setattr__(name, value)
	def __eq__(self, o):
		inner = object.__getattr__(self, "inner")
		if o is type:
			return type(inner) == o
		else:
			return inner == o

class PlayerCharacter(VirtualCharacterSheet.PlayerCharacter):
	def __init__(self, name, player):
		inner = VirtualCharacterSheet.PlayerCharacter(name, player)
		self = dyn(inner)
