
from VirtualCharacterSheet.Net.ApiHost import StartHost

from core.shell import add_base, PyCli

global task

def cmd_host(args, **kwargs):
	global task
	task = StartHost()

netshell_dict = add_base({
	'host': cmd_host
}, prune = ['net', 'view'])

netshell = PyCli(netshell_dict, shout = False, colon = False, name = 'Network Shell')
