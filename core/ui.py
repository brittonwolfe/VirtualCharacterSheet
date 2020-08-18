clr.AddReference('vcs')
from VirtualCharacterSheet.Forms import AbstractUi, AbstractUiFactory
from VirtualCharacterSheet.Terminal import AbstractTui

class PyUiFactory(AbstractUiFactory):
	constructor = None
	def __init__(self, constructor):
		self.constructor = constructor
	def Create(self, content):
		return self.constructor(content)

class PyTui(AbstractTui):
	pass

class PyGui(AbstractUi):
	#window = Tk()
	components = []
	def __init__(self):
		pass
	def append(self, component):
		self.components.append(component)
	def pack(self):
		for component in self.components:
			component.pack()
	def Render(self, content = None):
		self.pack()
	def Close(self):
		self.window.destroy()

class PyCharacterSheet(PyGui):
	character = None
	def __init__(self, character):
		self.character = character
