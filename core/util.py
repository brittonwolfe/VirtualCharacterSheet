
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
	"""	Creates a new `Brew` object.
	Parameters
	----------
	string: str
		The unique name of the Brew object being defined.
	Returns
	-------
	Brew
		The constructed and registered Brew object.
	See Also
	--------
	`VCS.Brew` : Brew Object. See `Data.cs`.
	`core.util._brew` : Gets a Brew object by name.
	"""
	return Brew(string)
#	def is_brewing():
#		return Scripting.Brewing

# global helpers
def readl(prompt: str):
	return Util.readl(prompt)
def roll(sides, count = None):
	"""	Performs a roll with the given number of `sides`-sided dice.
	"""
	if count:
		return Util.rolln(sides, count)
	return Util.roll(sides)

# viewers
def view(obj: object, brew: Brew = None):
	return Util.view(obj, brew)

# getters
def _brew(name: str):
	return Util._brew(name)
def _c(id: str):
	return Util._c(id)

# def object methods
def def_c(name: str, player: str):
	return PlayerCharacter(name, player)
