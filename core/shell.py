clr.AddReference('vcs')
from os import system
from shlex import split
from traceback import print_exc
from VirtualCharacterSheet import AbstractTui
from VirtualCharacterSheet.Data import AllBrews
from VirtualCharacterSheet.IO import File

class PyTui(AbstractTui):
	commands = {}
	show_output = False
	colon_escape = False
	def __init__(self, dict, shout = False, colon = False):
		self.commands = dict.copy()
		self.show_output = shout
		self.colon_escape = colon
	def Handle(self, command):
		if self.colon_escape and command.startswith(':'):
			exec(command[1:len(command)])
			return
		full = split(command)
		output = None
		try:
			output = self.commands[full[0]](full[1:len(full)])
		except Exception:
			print(print_exc())
		if self.show_output and output is not None:
			print(output)
		return output

def resolve_def(args):
	pass

def cmd_brew(args):
	if args[0].lower() == 'load':
		if len(args) != 2:
			print("invalid number of arguments")
		file = File(args[1])
		if not file.Exists():
			print('the file does not exist!')
			return
		brew.load(file.Path)
		print('Successfully loaded "' + args[1] + '"')
	if args[0].lower() == 'list':
		return AllBrews()

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

def cmd_view(args):
	print("not implemented")

basic_shell_dict = {
	'brew': cmd_brew,
	'roll': cmd_roll,
	'view': cmd_view
}
basic_shell = PyTui(basic_shell_dict, shout = True, colon = True)

def add_base(dict):
	output = basic_shell_dict.copy()
	output.update(dict)
	return output

def shell(tui = basic_shell):
	while True:
		line = readl('> ')
		if line == 'exit':
			return
		if line == 'clear':
			system('clear')
			continue
		tui.Handle(line)

if _setting._is_main:
	shell()
