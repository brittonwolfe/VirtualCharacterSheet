clr.AddReference('vcs')
from VirtualCharacterSheet.Net.ApiHost import StartHost

global task

def cmd_host(args, **kwargs):
	global task
	task = StartHost()

def cmd_stop(args, **kwargs):
	global task
	if task is None:
		print('No host task is running!')
		return
	task.Cancel()
	task.Dispose()
	task = None

netshell_dict = add_base({
	'host': cmd_host,
	'stop': cmd_stop
}, prune = ['net', 'view'])

netshell = PyCli(netshell_dict, shout = False, colon = False, name = 'Network Shell')

def dispose(self):
	global task
	if task is None:
		task.Cancel()
		task.Dispose()

netshell.__exit__ = dispose
