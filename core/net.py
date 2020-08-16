clr.AddReference('vcs')

netshell_dict = add_base({
	'host': cmd_host
}, prune = ['net', 'view'])

netshell = PyTui(netshell_dict, shout = False, colon = False)
