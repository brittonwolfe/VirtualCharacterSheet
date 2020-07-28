from core.shell import PyTui, add_base, cmd_roll

STAT_LIST = [ 'str', 'strength', 'dex', 'dexterity', 'con', 'constitution', 'int', 'intelligence', 'wis', 'wisdom', 'cha', 'charisma']
SHORTHAND_MAP = { 'str': 'Strength', 'dex': 'Dexterity', 'con': 'Constitution', 'int': 'Intelligence', 'wis': 'Wisdom', 'cha': 'Charisma' }

def get_default_local_character():
	if hasattr(local.This, 'Character'):
		return local.This.Character
	elif hasattr(local.This, 'character'):
		return local.This.character

def stat_check(args, character = None):
	'''stat [name]
	todo doc
	'''
	if character is None:
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
	mod = 0
	for arg in args[1:]:
		if arg.startswith('+'):
			mod += int(arg[1:])
		if arg.startswith('-'):
			mod -= int(arg[1:])
		else:
			if times is not None:
				print('too many times arguments given')
				return
			times = int(arg)
	output = 0
	for _ in range(times or 1):
		output += character.DoBehavior(stat + 'Check')
	return output

def versatile_roll(args, character = None):
	'''roll [stat] ...
	a versatile wrapper around the base roll command
	that allows the user to specify a stat to use for
	the roll as well.

	stat - the name of the stat to use
	'''
	if args[0].lower() in STAT_LIST:
		return stat_check(args, character = character)
	else:
		return cmd_roll(args)

character_tui = PyTui(add_base({
	'check': stat_check,
	'roll': versatile_roll
}), shout = True)
