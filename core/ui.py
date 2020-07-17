from tkinter import Tk

class Gui():
	window = Tk()
	components = []
	def __init__(self):
		pass
	def append(self, component):
		self.components.append(component)
	def pack(self):
		for component in self.components:
			component.pack()

class CharacterSheet(Gui):
	pass