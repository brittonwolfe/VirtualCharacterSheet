clr.AddReference('vcs')

from shell import PyCli, add_base

def cmd_host():
	pass

netshell_dict = add_base({
	'host': cmd_host
}, prune = ['net', 'view'])

netshell = PyCli(netshell_dict, shout = False, colon = False)
