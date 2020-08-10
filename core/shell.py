clr.AddReference('vcs')
from inspect import getargspec
from os import getcwd, system
from os.path import isdir
from shlex import split
from traceback import print_exc
from VirtualCharacterSheet import AbstractTui, PlayerCharacter
from VirtualCharacterSheet.Core import View
from VirtualCharacterSheet.Data import AllBrews, GetBrew, GetCharacter, GetItem, HasBrew, HasCharacter, HasItem
from VirtualCharacterSheet.IO import File, Dir

class PyTui(AbstractTui):
	"""Used to create a shell interface for interacting with the VCS environment."""
	commands = {}
	show_output = False
	colon_escape = False
	def __init__(self, dictionary, shout = False, colon = False, name = 'Unnamed Shell'):
		self.commands = dictionary.copy()
		self.show_output = shout
		self.colon_escape = colon
		self.name = name
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
	def Handle(self, command, **kwargs):
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
			self.help_command(full[1], **kwargs)
			return
		output = None
		try:
			cmd = self.commands[full[0]]
			if getargspec(cmd).keywords is not None:
				output = cmd(full[1:], **kwargs)
			else:
				output = cmd(full[1:])
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
		check_function = lambda s: True
		if arg.startswith('-C[') and arg.endswith(']'):
			replace_function = GetCharacter
			check_function = HasCharacter
		if arg.startswith('-I[') and arg.endswith(']'):
			replace_function = GetItem
			check_function = HasItem
		if arg.startswith('-b[') and arg.endswith(']'):
			replace_function = GetBrew
			check_function = HasBrew
		identity = arg[3:-1]
		if replace_function is not None:
			if not check_function(identity):
				print('nothing found for identity of "' + identity + '"')
				return
			args[i] = replace_function(identity)

def cmd_brew(args):
	"""brew [list | load <path> | info <identifier>]
	list	lists all loaded brews
	load	loads the brew at the given <path>
	info	shows information about the specified brew
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
	if args[0].lower() == 'info':
		brews = args[1:]
		for brew_name in brews:
			if HasBrew(brew_name):
				obj = GetBrew(brew_name)
				print(brew_name)
				print(obj.Meta.Title)
				if hasattr(obj.Meta, 'Version'):
					print('v' + obj.Meta.Version)
				if hasattr(obj.Meta, 'Author'):
					print('By', obj.Meta.Author)
				else:
					print('(no author specified)')
				if hasattr(obj.Meta, 'Description'):
					print(obj.Meta.Description)
				print(obj.Meta.Dir.Path)
			else:
				print('no brew called ' + brew_name + ' was found')

def cmd_load(args):
	typeof = None
	if not args[0].startswith('-'):
		print('invalid argument', args[0])
		return
	if args[0] == '-C':
		typeof = PlayerCharacter
	if typeof is None:
		print('unsupported type argument')
		return
	did_load = load_object(typeof, args[1])
	if did_load:
		print('loaded successfully!')
	else:
		print('did not load')

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

def cmd_which(args):
	if args[0].lower() == 'shell':
		print(local.__shellname__)
		return
	pass

basic_shell_dict = {
	'brew': cmd_brew,
	'load': cmd_load,
	'roll': cmd_roll,
	'save': cmd_save,
	'view': cmd_view,
	'which': cmd_which
}
basic_shell = PyTui(basic_shell_dict, shout = True, colon = True, name = 'Base Shell')

def add_base(dictionary):
	output = basic_shell_dict.copy()
	output.update(dictionary)
	return output

def non_loop_shell(tui, **kwargs):
	local.__shellname__ = tui.name
	line = readl('> ')
	if line == 'exit':
		del local.__shellname__
		return True
	if line == 'clear':
		system('clear')
		del local.__shellname__
		return False
	tui.Handle(line, **kwargs)
	del local.__shellname__
	return False

def shell(tui = basic_shell, **kwargs):
	breaks = False
	while not breaks:
		breaks = non_loop_shell(tui, **kwargs)
