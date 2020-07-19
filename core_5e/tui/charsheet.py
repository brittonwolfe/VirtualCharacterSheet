clr.AddReference('vcs')
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm, TerminalView
from core.ui import PyCharacterSheet, PyUiFactory

from charsheet_graphics import header

core = _brew('core_5e')

basic = TerminalView(header)

class Core5eCharacterSheet(PyCharacterSheet):
	def __init__(self, character):
		super().__init__(self, character)
		print('foo', self.character.Name)

core.AddView(PlayerCharacter, PyUiFactory(Core5eCharacterSheet))
