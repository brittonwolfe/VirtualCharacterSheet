core = brew.def_brew("core_5e")
core.Meta.Title = "D&D Fifth Edition"
core.Meta.Description = "The core rules for Dungeons and Dragons Fifth Edition"
core.Meta.Dir = brew.Path

def InjectChecks(character):
	character.AddBehavior("StrengthCheck", lambda self: (roll(20) + self.STR))
	character.AddBehavior("DexterityCheck", lambda self: (roll(20) + self.DEX))
	character.AddBehavior("ConstitutionCheck", lambda self: (roll(20) + self.CON))
	character.AddBehavior("IntelligenceCheck", lambda self: (roll(20) + self.INT))
	character.AddBehavior("WisdomCheck", lambda self: (roll(20) + self.WIS))
	character.AddBehavior("CharismaCheck", lambda self: (roll(20) + self.CHA))

def generate_save(name, check):
	def output(self):
		mods = 0
		if name in self.Info.proficient_saves:
			mods += self.Info.proficiency_bonus
		return (check() + mods)
	return output

def InjectSaves(character):
	character.Info.proficient_saves = []
	character.AddBehavior("save_strength", generate_save("strength", character.StrengthCheck))
	character.AddBehavior("save_dexterity", generate_save("dexterity", character.DexterityCheck))
	character.AddBehavior("save_constitution", generate_save("constitution", character.ConstitutionCheck))
	character.AddBehavior("save_intelligence", generate_save("intelligence", character.IntelligenceCheck))
	character.AddBehavior("save_wisdom", generate_save("wisdom", character.WisdomCheck))
	character.AddBehavior("save_charisma", generate_save("charisma", character.CharismaCheck))

def generate_skill(name, check):
	def output(self):
		mods = 0
		if name in self.Info.proficient_skills:
			mods += self.Info.proficiency_bonus
		return (check() + mods)
	return output

def InjectSkills(character):
	character.Info.proficient_skills = []
	character.AddBehavior("skill_acrobatics", generate_skill("acrobatics", character.DexterityCheck))
	character.AddBehavior("skill_animal_handling", generate_skill("animal handling", character.WisdomCheck))
	character.AddBehavior("skill_arcana", generate_skill("arcana", character.IntelligenceCheck))
	character.AddBehavior("skill_athletics", generate_skill("athletics", character.StrengthCheck))
	character.AddBehavior("skill_deception", generate_skill("deception", character.CharismaCheck))
	character.AddBehavior("skill_history", generate_skill("history", character.IntelligenceCheck))
	character.AddBehavior("skill_insight", generate_skill("insight", character.WisdomCheck))
	character.AddBehavior("skill_intimidation", generate_skill("intimidation", character.CharismaCheck))
	character.AddBehavior("skill_investigation", generate_skill("investigation", character.IntelligenceCheck))
	character.AddBehavior("skill_medicine", generate_skill("medicine", character.WisdomCheck))
	character.AddBehavior("skill_nature", generate_skill("nature", character.IntelligenceCheck))
	character.AddBehavior("skill_perception", generate_skill("perception", character.WisdomCheck))
	character.AddBehavior("skill_performance", generate_skill("performance", character.CharismaCheck))
	character.AddBehavior("skill_persuasion", generate_skill("persuasion", character.CharismaCheck))
	character.AddBehavior("skill_religion", generate_skill("religion", character.IntelligenceCheck))
	character.AddBehavior("skill_sleight_of_hand", generate_skill("sleight of hand", character.DexterityCheck))
	character.AddBehavior("skill_stealth", generate_skill("stealth", character.DexterityCheck))
	character.AddBehavior("skill_survival", generate_skill("survival", character.WisdomCheck))

core.AddCharacterInjector(InjectChecks)
core.AddCharacterInjector(InjectSaves)
core.AddCharacterInjector(InjectSkills)

# set up views

import classes.fighter
import classes.fighter_battle_master

import item.rapier