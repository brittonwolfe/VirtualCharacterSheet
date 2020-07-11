from core.shell import PyTui, add_base, cmd_roll

STAT_LIST = [ 'str', 'strength', 'dex', 'dexterity', 'con', 'constitution', 'int', 'intelligence', 'wis', 'wisdom', 'cha', 'charisma']

def stat_check(args):
	stat = None
	if args[0].lower() in STAT_LIST:
		stat = args[0].lower()
	if stat is None:
		print('No valid stat provided')
		return
	pass #local.This.Character

def versatile_roll(args):
	if args[0].lower() in STAT_LIST:
		return stat_check(args)
	else:
		return cmd_roll(args)

character_tui = PyTui(add_base({
	'check': stat_check,
	'roll': versatile_roll
}))
