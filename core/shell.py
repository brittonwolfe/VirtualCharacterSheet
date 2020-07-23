clr.AddReference('vcs')
from os import getcwd, system
from os.path import isdir
from shlex import split
from traceback import print_exc
from VirtualCharacterSheet import AbstractTui
from VirtualCharacterSheet.Core import View
from VirtualCharacterSheet.Data import AllBrews, GetBrew, GetCharacter, GetItem, HasBrew, HasCharacter, HasItem
from VirtualCharacterSheet.IO import File, Dir

class PyTui(AbstractTui):
	commands = {}
	show_output = False
	colon_escape = False
	def __init__(self, dict, shout = False, colon = False):
		self.commands = dict.copy()
		self.show_output = shout
		self.colon_escape = colon
	def help_command(self, command):
		"""help [command]
	Shows help text for the given command.
		"""
		if command is None or command == '':
			print(self.help_command.__doc__)
			return
		if not command in self.commands:
			print('command not found')
		print(self.commands[command].__doc__)
	def Handle(self, command):
		if self.colon_escape and command.startswith(':'):
			exec(command[1:])
			return
		full = split(command)
		if full[0] == 'help':
			if len(full) == 1:
				self.help_command(None)
				return
			if len(full) > 2:
				print('too many arguments supplied')
				return
			self.help_command(full[1])
			return
		output = None
		try:
			output = self.commands[full[0]](full[1:])
		except Exception:
			print(print_exc())
			return
		if self.show_output and output is not None:
			print(output)
		return output

def resolve_ref(args):
	for i in range(len(args)):
		arg = args[i]
		replace_function = None
		if arg.startswith('-C[') and arg.endswith(']'):
			replace_function = GetCharacter
		if arg.startswith('-I[') and arg.endswith(']'):
			replace_function = GetItem
		if arg.startswith('-b[') and arg.endswith(']'):
			replace_function = GetBrew
		if replace_function is not None:
			args[i] = replace_function(arg[3:-1])

def cmd_brew(args):
	"""brew [list | load <path>]
	list	lists all loaded brews
	load	loads the brew at the given <path>
	"""
	if args[0].lower() == 'load':
		if len(args) != 2:
			print("invalid number of arguments")
		file = File(args[1])
		if not file.Exists():
			dir = Dir(args[1])
			if dir.Exists():
				path = dir.Path
				if not path.endswith('/'):
					path += '/'
				path += 'brew.py'
				cmd_brew(['load', path])
				return
			else:
				# if for some stupid reason we can't get the relative path, get the absolute one!
				dir = Dir(getcwd() + '/' + args[1])
				if dir.Exists():
					cmd_brew([args[0], args[1]])
					return
			print('the file does not exist!')
			return
		brew.load(file.Path)
		print('Successfully loaded "' + args[1] + '"')
	if args[0].lower() == 'list':
		return AllBrews()

def cmd_roll(args):
	"""roll [times] d<sides>
	sides	the number of sides the die should have
	times	the number of times to roll the die
	"""
	if len(args) == 1:
		num = None
		if args[0].lower().startswith('d'):
			num = int(args[0][1:])
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
				d = int(arg[1:])
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

def cmd_save(args):
	if len(args) > 2:
		print('too many arguments!')
		return
	resolve_ref(args)
	did_save = save_object(args[0], args[1])
	if did_save:
		print('saved successfully.')
	else:
		print('did not save successfully.')
	
def cmd_view(args):
	resolve_ref(args)
	View(args[0])

basic_shell_dict = {
	'brew': cmd_brew,
	'roll': cmd_roll,
	'save': cmd_save,
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
