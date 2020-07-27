clr.AddReference('vcs')
from VirtualCharacterSheet import PlayerCharacter
from VirtualCharacterSheet.Forms import TerminalForm, TerminalView
from core.ui import PyCharacterSheet, PyUiFactory

core = _brew('core_5e')

class Core5eCharacterSheet(PyCharacterSheet):
	def __init__(self, obj):
		super().__init__(self, obj)
		print('foo', self.character.Name)
		self.basic_view()
	def render_header(self):
		width = TerminalForm.GetTerminalWidth()
		full_line = '#' * width
		return (full_line + '\n' + self.character.Name)
	def stat_section(self):
		pass
	def basic_view(self):
		print(self.render_header())

core.AddView(PlayerCharacter, PyUiFactory(Core5eCharacterSheet))
