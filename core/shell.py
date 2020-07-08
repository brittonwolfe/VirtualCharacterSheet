from shlex import split
from VirtualCharacterSheet.Terminal import AbstractTui

class PyTui(AbstractTui):
	commands = {}
	def __init__(self,dict):
		for (k,v) in dict:
			self.commands[k] = v
	def Handle(self,command):
		full = split(command)
		self.commands[full[0]](full[1:-1])

def cmd_roll(args):
	if len(args) == 1:
		if args[0] == '--help':
			print("Usage: roll [count] [d]<sides>\nif you specify a count, the number of sides must have the 'd' prefix.")
			return
		num = None
		if args[0].lower().startswith('d'):
			num = int(args[0][1:-1])
		else:
			num = int(args[0])
		return roll(num)
	n = None
	d = None
	for arg in args:
		if arg.lower().startswith('d'):
			if d is None:
				d = int(arg[1:-1])
			else:
				print('too many arguments!')
				return
		else:
			if n is None:
				n = int(arg)
			else:
				print('too many arguments!')
				return
	if d is None:
		print('not enough arguments!')
		return
	if n is None:
		return roll(d)
	else:
		return rolln(n, d)

basic_shell = PyTui({
	"roll": cmd_roll
})
