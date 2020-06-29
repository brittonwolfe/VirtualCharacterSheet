from VirtualCharacterSheet.Forms import TerminalForm, TerminalGraphic

def header_render():
	width = TerminalForm.GetTerminalWidth()
	return	("#" * width)

header = TerminalGraphic(header_render)
