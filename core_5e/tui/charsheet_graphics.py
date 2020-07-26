
from VirtualCharacterSheet.Forms import TerminalForm, TerminalGraphic

def header_render():
	width = TerminalForm.GetTerminalWidth()
	full_line = '#' * width
	return full_line

header = TerminalGraphic(header_render)
