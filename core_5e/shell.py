from core.shell import PyTui, add_base, cmd_roll

def stat_check(args):
	local.This.Character

character_tui = PyTui(add_base({
	'check': stat_check
}))
