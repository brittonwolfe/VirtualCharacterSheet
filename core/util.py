from VirtualCharacterSheet import Brew
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet import Scripting
from VirtualCharacterSheet import Util

from VirtualCharacterSheet import FileScript
from VirtualCharacterSheet.IO import File

class dynobj(object):
	pass

# util objects
brew = dynobj()
local = dynobj()
_setting = dynobj()

# brew helper
def def_brew(string):
	return Brew(string)
def is_brewing():
	return Scripting.Brewing
def load_brew(name: str):
	mod = name.replace("/",".").rstrip(".py")
	return __import__(mod)

brew.load = load_brew

# def global helpers
def readl(prompt: str):
	return Util.readl(prompt)
def roll(sides):
	return Util.roll(sides)
def rolln(sides, count):
	return Util.rolln(sides, count)

# def viewers
def view(obj: object, brew: Brew = None):
	return Util.view(obj, brew)

# def getters
def _brew(name):
	return Util._brew(name)

# def object methods
def def_c(name: str, player: str):
	return PlayerCharacter(name, player)
