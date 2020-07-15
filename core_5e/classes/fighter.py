fighter = def_class("core_5e:fighter")
fighter.Name = "Fighter"
fighter.HitDie = 10
fighter.Info.Book = "Player's Handbook"
fighter.Info.Pages = "70-75"

fighter.Meta.proficient_skills = []
fighter.Meta.skill_choices = 2

def class_init(character):
	if not character.class_list:
		character.Meta.class_list = []
	character.Meta.proficiency_bonus = 2
	character.Meta.proficient_saves.append('strength')
	character.Meta.proficient_saves.append('constitution')
