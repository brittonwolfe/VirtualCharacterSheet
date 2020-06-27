
def strength():
	return this.Character.StrengthCheck()
def dexterity():
	return this.Character.DexterityCheck()
def constitution():
	return this.Character.ConstitutionCheck()
def intelligence():
	return this.Character.IntelligenceCheck()
def wisdom():
	return this.Character.WisdomCheck()
def charisma():
	return this.Character.CharismaCheck()

def setup_tui(form):
	form.SetupTui(
		('check_strength', strength),
		('check_dexterity', dexterity),
		('check_constitution', constitution),
		('check_intelligence', intelligence),
		('check_wisdom', wisdom),
		('check_charisma', charisma)
	)
