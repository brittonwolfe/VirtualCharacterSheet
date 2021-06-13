# We don't actually use GTKSharp yet so...
# import clr
# clr.AddReference('GtkSharp')

from VirtualCharacterSheet.Forms import AbstractGui, IUiFactory
from VirtualCharacterSheet.Terminal import AbstractTui

from core.util import dynobj, local

class PyUiFactory(IUiFactory):
	__metaclass__ = IUiFactory
	__namespace__ = "PyUiFactory"
	constructor = None
	def __init__(self, constructor):
		self.constructor = constructor
	def Create(self, content):
		return self.constructor(content)

class PyTui(AbstractTui):
	content = None
	__namespace__ = "PyTui"
	def __init__(self, content):
		self.content = content
	def Render(self):
		pass

class PyGui(AbstractGui):
	content = None
	def __init__(self, name = None, **kwargs):
		AbstractGui.__init__(self, name = name or 'VCS Generic PyGui')
		self.content = {item for item in kwargs}
	def append(self, component):
		self.Components.append(component)
		self.Window.Add(component)
	def resize(self, x = None, y = None):
		tup = AbstractGui.GetSize(self)
		self.Window.Resize(x or tup.Item1, y or tup.Item2)
