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
	if len(stat) == 3:
		stat = STAT_LIST[STAT_LIST.index(stat) + 1].capitalize()
	output = 0
	for _ in range(times or 1):
		output += local.This.Character.DoBehavior(stat + 'Check')
	return output

def versatile_roll(args):
	if args[0].lower() in STAT_LIST:
		return stat_check(args)
	else:
		return cmd_roll(args)

character_tui = PyTui(add_base({
	'check': stat_check,
	'roll': versatile_roll
}), shout = True)

def do_something():
	if local.curr is None:
		return
	from core.shell import shell
	class _():
		pass
	local.This = _()
	local.This.Character = local.curr
	shell(character_tui)

local.foo = do_something
