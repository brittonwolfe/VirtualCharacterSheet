
from VCS import PlayerCharacter

from core.ui import PyTui

class Core5eCharacterDesigner(PyTui):
	def __init__(self, content = None):
		if content is not None:
			self.content = content
		else:
			self.content = PlayerCharacter()
	def Render(self):
		pass
