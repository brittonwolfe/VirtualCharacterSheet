from core.shell import PyTui, add_base, cmd_roll

STAT_LIST = [ 'str', 'strength', 'dex', 'dexterity', 'con', 'constitution', 'int', 'intelligence', 'wis', 'wisdom', 'cha', 'charisma']

def stat_check(args):
	stat = None
	if args[0].lower() in STAT_LIST:
		stat = args[0].lower().capitalize()
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
	for i in range(times or 1):
		output = local.This.Character[stat + 'Check']() + mod
	return output

def versatile_roll(args):
	if args[0].lower() in STAT_LIST:
		return stat_check(args)
	else:
		return cmd_roll(args)

character_tui = PyTui(add_base({
	'check': stat_check,
	'roll': versatile_roll
}))
