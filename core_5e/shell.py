from core.shell import PyCli, add_base, cmd_roll

from VirtualCharacterSheet.Util import local

STAT_LIST = [ 'str', 'strength', 'dex', 'dexterity', 'con', 'constitution', 'int', 'intelligence', 'wis', 'wisdom', 'cha', 'charisma']
SHORTHAND_MAP = { 'str': 'Strength', 'dex': 'Dexterity', 'con': 'Constitution', 'int': 'Intelligence', 'wis': 'Wisdom', 'cha': 'Charisma' }

def get_default_local_character():
	if hasattr(local.This, 'Character'):
		return local.This.Character
	elif hasattr(local.This, 'character'):
		return local.This.character

def stat_check(args, **kwargs):
	'''check [name]
	todo doc
	'''
	character = None
	if 'character' in kwargs:
		character = kwargs['character']
	else:
		character = get_default_local_character()
	stat = None
	if args[0].lower() in STAT_LIST:
		stat = args[0].lower()
		if len(stat) == 3:
			stat = SHORTHAND_MAP[stat]
		else:
			stat = stat.lower().capitalize()
	else:
		print('No valid stat provided')
		return
	times = None
	output = 0
	for arg in args[1:]:
		if arg.startswith('+'):
			output += int(arg[1:])
		elif arg.startswith('-'):
			output -= int(arg[1:])
		else:
			if times is not None:
				print('too many times arguments given')
				return
			times = int(arg)
	for _ in range(times or 1):
		output += character.DoBehavior(stat + 'Check')
	return output

def versatile_roll(args, **kwargs):
	'''roll [stat] ...
	a versatile wrapper around the base roll command
	that allows the user to specify a stat to use for
	the roll as well.

	stat - the name of the stat to use
	'''
	if args[0].lower() in STAT_LIST:
		return stat_check(args, **kwargs)
	else:
		return cmd_roll(args)

def cmd_skill(args, **kwargs):
	'''skill [name]
	'''
	character = None
	if 'character' in kwargs:
		character = kwargs['character']
	else:
		character = get_default_local_character()
	if len(args) != 1:
		return
	methodname = ('skill_' + args[0].lower())
	if not character.HasBehavior(methodname):
		print(args[0] + ' is not a valid skill')
		return
	return character.DoBehavior(methodname)

character_cli = PyCli(
	add_base({
		'check': stat_check,
		'roll': versatile_roll,
		'skill': cmd_skill
	}, prune = ['view']),
	shout = True,
	name = 'Core 5E Character Shell'
)
