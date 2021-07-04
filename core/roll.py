from core.util import roll

def Roll():
	def __init__(self):
		self.parts = []
	def __add__(self, other):
		total = 0
		for part in parts:
			if part is Die:
				total += part.roll()
			if part is Mod:
				total += part.val
			if part is ScriptedMod:
				total += part.val()
		return total

def Mod():
	def __init__(self, n: int):
		self.val = n
	def __repr__(self):
		if self.val < 0:
			return f'{self.val}'
		return f'+{self.val}'

def ScriptedMod():
	def __init__(self, f):
		self.f = f
	def val(self):
		return self.f()
	def __repr__(self):
		val = self.f()
		if val < 0:
			return f'{val}'
		return f'+{val}'

def Die():
	def __init__(self, sides: int):
		self.sides = sides
	def roll(self, count = 1):
		return roll(self.sides, count)
	def repr(self):
		return f'd{self.sides}'
