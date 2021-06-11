from VirtualCharacterSheet.Event import InjectionEvent

def injection_event(function):
	return InjectionEvent(function)
