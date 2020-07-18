clr.AddReference('vcs')
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm, TerminalView

from charsheet_graphics import header

core = _brew('core_5e')

basic = TerminalView(header)

class CharacterSheet(TerminalForm):
	def Render(self, obj):
		print(obj.Name)
	def Close(self):
		pass

char_tui = CharacterSheet()

core.AddView(PlayerCharacter, char_tui)
