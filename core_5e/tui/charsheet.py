clr.AddReference('vcs')
from os import system

from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm, TerminalView
from core.ui import PyCharacterSheet, PyUiFactory
from core.shell import non_loop_shell

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
		output = full_line[0:4] + name + full_line[4 + len(name):] + '\n'
		player = self.character.Player
		output += '| ' + player + (' ' * (width - (player.Length + 3))) + '|'
		output += full_line
		return output
	def render_stat_section(self):
		output =	'=Stats=\n'
		output +=	'| STR |\n'
		output +=	'|  ' + str(self.character.Strength)
		if self.character.Strength < 10:
			output += ' '
		output += ' |\n'
		output +=	'| DEX |\n'
		output +=	'|  ' + str(self.character.Dexterity)
		if self.character.Dexterity < 10:
			output += ' '
		output += ' |\n'
		output +=	'| CON |\n'
		output +=	'|  ' + str(self.character.Constitution)
		if self.character.Constitution < 10:
			output += ' '
		output += ' |\n'
		output +=	'| INT |\n'
		output +=	'|  ' + str(self.character.Intelligence)
		if self.character.Intelligence < 10:
			output += ' '
		output += ' |\n'
		output +=	'| WIS |\n'
		output +=	'|  ' + str(self.character.Wisdom)
		if self.character.Wisdom < 10:
			output += ' '
		output += ' |\n'
		output +=	'| CHA |\n'
		output +=	'|  ' + str(self.character.Charisma)
		if self.character.Charisma < 10:
			output += ' '
		output += ' |\n'
		output += ('=' * 7)
		return output
	def basic_view(self):
		system('clear')
		TerminalForm.SetCursorPosition(x = 0, y = 0)
		print(self.render_header())
		TerminalForm.SetCursorPosition(x = 0, y = 4)
		print(self.render_stat_section())
		TerminalForm.SetCursorPosition()
		breaks = False
		while not breaks:
			TerminalForm.SetCursorPosition(x = 9)
			breaks = non_loop_shell(character_tui, character = self.character)

core.AddView(PlayerCharacter, PyUiFactory(lambda content: Core5eCharacterSheet(content)))
