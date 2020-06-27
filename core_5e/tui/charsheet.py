clr.AddReference('vcs')
from VirtualCharacterSheet.Forms import TerminalView, CharacterSheet

#from charsheet_graphics import header
from charsheet_tui import setup_tui

core = _brew('core_5e')

basic = TerminalView()

char_tui = CharacterSheet([('basic', basic)])
setup_tui(char_tui)

#core.AddTui(char_tui)
