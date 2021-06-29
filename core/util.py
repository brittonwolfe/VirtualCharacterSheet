from VCS import Brew
from VCS import PlayerCharacter
from VCS import Scripting
from VCS import Util

# util objects
brew = Util.brew
local = Util.local
_setting = Util._setting

# brew helpers
def def_brew(string):
	return Brew(string)
#	def is_brewing():
#		return Scripting.Brewing

# global helpers
def readl(prompt: str):
	return Util.readl(prompt)
def roll(sides, count = None):
	if count:
		return Util.rolln(sides, count)
	return Util.roll(sides)

# viewers
def view(obj: object, brew: Brew = None):
	return Util.view(obj, brew)

# getters
def _brew(name):
	return Util._brew(name)

# def object methods
def def_c(name: str, player: str):
	return PlayerCharacter(name, player)
