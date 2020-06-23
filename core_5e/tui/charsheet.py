clr.AddReference('vcs')
from VirtualCharacterSheet.Forms import TerminalView, CharacterSheet

from charsheet_graphics import header

core = _brew('core_5e')

basic = TerminalView()

char_tui = CharacterSheet()

#core.AddTui(char_tui)
