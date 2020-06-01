fighter = def_class("core_5e:fighter")
fighter.Name = "Fighter"
fighter.HitDie = 10
fighter.Info.Book = "Player's Handbook"
fighter.Info.Pages = "70-75"

def class_init(character):
	character.Info.proficiency_bonus = 2
	character.Info.proficient_saves.append(save_strength)
	character.Info.proficient_saves.append(save_constitution)
