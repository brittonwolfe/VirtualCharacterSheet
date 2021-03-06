
from os import system

from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm
from VirtualCharacterSheet.Util import _brew, readl

from core.ui import PyTui, PyUiFactory
from core.shell import non_loop_shell

from core_5e.shell import character_cli

core = _brew('core_5e')

class Core5eCharacterSheet(PyTui):
	def __init__(self, content):
		self.content = content
		self.view = 0
	def render_header(self):
		width = TerminalForm.GetTerminalWidth()
		full_line = '=' * width
		player = self.content.Player
		name = self.content.Name
		output = full_line[0:4] + name + full_line[4 + len(name):] + '\n'
		output += '| ' + player + (' ' * (width - (player.Length + 3))) + '|'
		output += full_line
		return output
	def render_stat_section(self):
		character = self.content
		output =	'=Stats=\n'
		output +=	'| STR |\n'
		output +=	'|  ' + str(character.Strength)
		if character.Strength < 10:
			output += ' '
		output += ' |\n'
		output +=	'| DEX |\n'
		output +=	'|  ' + str(character.Dexterity)
		if character.Dexterity < 10:
			output += ' '
		output += ' |\n'
		output +=	'| CON |\n'
		output +=	'|  ' + str(character.Constitution)
		if character.Constitution < 10:
			output += ' '
		output += ' |\n'
		output +=	'| INT |\n'
		output +=	'|  ' + str(character.Intelligence)
		if character.Intelligence < 10:
			output += ' '
		output += ' |\n'
		output +=	'| WIS |\n'
		output +=	'|  ' + str(character.Wisdom)
		if character.Wisdom < 10:
			output += ' '
		output += ' |\n'
		output +=	'| CHA |\n'
		output +=	'|  ' + str(character.Charisma)
		if character.Charisma < 10:
			output += ' '
		output += ' |\n'
		output += ('=' * 7)
		return output
	def render_skill_section(self):
		width = TerminalForm.GetTerminalWidth() - 8
		full_line = '=' * width
		output = full_line[0:4] + 'Skills' + full_line[10:] + '\n'
		output += full_line
		return output
	def basic_view(self):
		TerminalForm.SetCursorPosition(x = 0, y = 0)
		print(self.render_header())
		TerminalForm.SetCursorPosition(x = 0, y = 3)
		print(self.render_stat_section())
		TerminalForm.SetCursorPosition(x = 8, y = 3)
		render = self.render_skill_section()
		for line in render.split('\n'):
			TerminalForm.SetCursorPosition(x = 8)
			print(line)
	def Render(self):
		TerminalForm.Clear()
		breaks = False
		while not breaks:
			if self.view == 0:
				self.basic_view()
			TerminalForm.SetCursorPosition(x = 0, y = 17)
			breaks = non_loop_shell(character_cli, character = self.content)
			if not breaks:
				readl('')
				TerminalForm.Clear()

core.AddView(PlayerCharacter, PyUiFactory(lambda content: Core5eCharacterSheet(content)))
