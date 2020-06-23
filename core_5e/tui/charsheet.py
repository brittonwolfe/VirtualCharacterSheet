clr.AddReference('vcs')
from VirtualCharacterSheet.Forms import TerminalView, CharacterSheet

import charsheet_header

core = _brew('core_5e')

char_tui = CharacterSheet()

#core.AddTui(char_tui)