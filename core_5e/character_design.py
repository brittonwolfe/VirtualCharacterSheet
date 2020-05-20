
print('D&D 5E Character Designer')
name = input('Please input your character name: ')
player = input('Please input your name: ')
character = def_c(name, player)

valid_inputs = ['manual','script']
mode = None
while mode not in valid_inputs:
	mode = input('Select stat mode (manual / script): ')

def man_input(stat):
	temp = "null"
	while (not temp.isdecimal()) and ('.' not in temp) and (int(temp) > 0 and int(temp) < 30):
		STR = input(stat + ': ')
	return byte(int(temp))
	

if mode == 'manual':
	character.Strength = man_input('Strength')
	character.Dexterity = man_input('Dexterity')
	character.Constitution = man_input('Constitution')
	character.Intelligence = man_input('Intelligence')
	character.Wisdom = man_input('Wisdom')
	character.Charisma = man_input('Charisma')

if mode == 'script':
	pass

# only do this if the user wants to
character.Info.Traits = input('Personality Trait(s): ')
character.Info.Ideals = input('Ideal(s): ')
character.Info.Bonds = input('Bond(s): ')
character.Info.Flaws = input('Flaw(s): ')
