clr.AddReference('vcs')
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm, TerminalView
from core.ui import PyCharacterSheet, PyUiFactory
from core.shell import shell

character_tui = brew.import_absolute('shell.py', 'character_tui')

core = _brew('core_5e')

class Core5eCharacterSheet(PyCharacterSheet):
	def __init__(self, character):
		self.character = character
		self.basic_view()
	def render_header(self):
		width = TerminalForm.GetTerminalWidth()
		full_line = '=' * width
		name = self.character.Name
		output = full_line[0:4] + name + full_line[3 + len(name):] + '\n'
		player = self.character.Player
		output += '| ' + player + (' ' * (width - (player.Length + 3))) + '|'
		return output
	def render_stat_section(self):
		pass
	def basic_view(self):
		print(self.render_header())
		shell(character_tui, character = self.character)

core.AddView(PlayerCharacter, PyUiFactory(lambda content: Core5eCharacterSheet(content)))
