clr.AddReference('vcs')
from shlex import split
from VirtualCharacterSheet import AbstractTui

class PyTui(AbstractTui):
	commands = {}
	show_output = False
	def __init__(self, dict, shout = False):
		self.commands = dict.copy()
		self.show_output = shout
	def Handle(self, command):
		full = split(command)
		output = self.commands[full[0]](full[1:len(full)])
		if self.show_output:
			print(output)
		return output

def cmd_roll(args):
	if len(args) == 1:
		if args[0] == '--help':
			print("Usage: roll [count] [d]<sides>\nif you specify a count, the number of sides must have the 'd' prefix.")
			return
		num = None
		if args[0].lower().startswith('d'):
			num = int(args[0][1:len(args[0])])
		else:
			if 'd' in args[0]:
				return cmd_roll(split(args[0].replace('d', ' d')))
			num = int(args[0])
		return roll(num)
	n = None
	d = None
	for arg in args:
		if arg.lower().startswith('d'):
			if d is None:
				d = int(arg[1:len(arg)])
			else:
				print('too many arguments!')
				return
		else:
			if n is None:
				n = int(arg)
			else:
				print('too many arguments!')
				return
	if d is None or n is None:
		print('not enough arguments!')
		return
	else:
		return rolln(n, d)

basic_shell_dict = {
	'roll': cmd_roll
}
basic_shell = PyTui(basic_shell_dict, shout = True)

def add_base(dict):
	output = basic_shell_dict.copy()
	output.update(dict)
	return output

def shell(tui = basic_shell):
	while True:
		line = readl('> ')
		if line == 'exit':
			return
		tui.Handle(line)

if _setting._is_main:
	shell()
