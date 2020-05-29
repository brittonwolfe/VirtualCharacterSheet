
print('D&D 5E Console Character Designer')
name = readl('Please input your character name: ')
player = readl('Please input your name: ')
character = def_c(name, player)

valid_inputs = ['manual', 'script']
mode = None
while mode not in valid_inputs:
	mode = readl('Select stat mode (manual / script): ')

def man_input(stat):
	temp = 'null'
	while (not temp.isdecimal()) or '.' in temp or (int(temp) < 0 or int(temp) > 30):
		temp = readl(stat + ': ')
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
# character.Info.Traits = input('Personality Trait(s): ')
# character.Info.Ideals = input('Ideal(s): ')
# character.Info.Bonds = input('Bond(s): ')
# character.Info.Flaws = input('Flaw(s): ')
# honestly tho probs better to just do this in a ui, but uh...
