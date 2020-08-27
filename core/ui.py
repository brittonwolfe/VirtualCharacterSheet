clr.AddReference('vcs')
clr.AddReference('GtkSharp')
from VirtualCharacterSheet.Forms import AbstractGui, AbstractUiFactory
from VirtualCharacterSheet.Terminal import AbstractTui

class PyUiFactory(AbstractUiFactory):
	constructor = None
	def __init__(self, constructor):
		self.constructor = constructor
	def Create(self, content):
		return self.constructor(content)

class PyTui(AbstractTui):
	content = None
	def __init__(self, content):
		self.content = content
	def Render(self):
		pass

class PyGui(AbstractGui):
	components = []
	content = None
	def __init__(self, name = None, **kwargs):
		AbstractGui.__init__(self, name = name or 'VCS Generic PyGui')
		self.content = {item for item in kwargs}
	def append(self, component):
		self.components.append(component)
		self.Window.Add(component)
	def resize(self, x = None, y = None):
		tup = AbstractGui.GetSize(self)
		self.Window.Resize(x or tup.Item1, y or tup.Item2)
