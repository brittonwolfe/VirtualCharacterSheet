clr.AddReference('vcs')
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalView

from charsheet_graphics import header

core = _brew('core_5e')

basic = TerminalView(header)

char_tui = None # CharacterSheet()

# core.AddTui(PlayerCharacter, char_tui)
