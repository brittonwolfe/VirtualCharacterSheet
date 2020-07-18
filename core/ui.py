clr.AddReference('vcs')
from VirtualCharacterSheet.Forms import AbstractUi

from tkinter import Tk

class PyGui(AbstractUi):
	window = Tk()
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
