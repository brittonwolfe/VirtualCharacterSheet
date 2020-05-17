core = brew.def_brew("core_5e")
core.Title = "D&D Fifth Edition"
core.Description = "The core rules for Dungeons and Dragons Fifth Edition"
core.Dir = brew.Path;

# I don't have the groundwork right now to actually define all the saves,
# so I'm doing some placeholders.
core.def_save("Strength", lambda src: src.Save.STR)
core.def_save("Dexterity", lambda src: src.Save.DEX)
core.def_save("Constitution", lambda src: src.Save.CON)
core.def_save("Intelligence", lambda src: src.Save.INT)
core.def_save("Wisdom", lambda src: src.Save.WIS)
core.def_save("Charisma", lambda src: src.Save.CHA)

# Again, the groundwork is a little more "there" for the skills, but
# I'm not really at the point where I can make those dynamic.
core.def_skill("Acrobatics", 0)
core.def_skill("Animal Handling", 4)
core.def_skill("Arcana", 3)
core.def_skill("Athletics", 0)
core.def_skill("Deception", 5)
core.def_skill("History", 3)
core.def_skill("Insight", 4)
core.def_skill("Intimidation", 5)
core.def_skill("Investigation", 4)
core.def_skill("Medicine", 4)
core.def_skill("Nature", 3)
core.def_skill("Perception", 4)
core.def_skill("Performance", 5)
core.def_skill("Persuasion", 5)
core.def_skill("Religion", 4)
core.def_skill("Sleight of Hand", 2)
core.def_skill("Stealth", 1)
core.def_skill("Survival", 4)
