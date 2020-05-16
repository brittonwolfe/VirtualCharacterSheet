brew.add_module("core_5e")

# I don't have the groundwork right now to actually define all the saves,
# so I'm doing some placeholders.
brew.def_save("Strength", 0)
brew.def_save("Dexterity", 1)
brew.def_save("Constitution", 2)
brew.def_save("Intelligence", 3)
brew.def_save("Wisdom", 4)
brew.def_save("Charisma", 5)

# Again, the groundwork is a little more "there" for the skills, but
# I'm not really at the point where I can make those dynamic.
brew.def_skill("Acrobatics", 1)
brew.def_skill("Animal Handling", 4)
brew.def_skill("Arcana", 3)
brew.def_skill("Athletics", 0)
brew.def_skill("Deception", 5)
brew.def_skill("History", 3)
brew.def_skill("Insight", 4)
brew.def_skill("Intimidation", 5)
brew.def_skill("Investigation", 4)
brew.def_skill("Medicine", 4)
brew.def_skill("Nature", 3)
brew.def_skill("Perception", 4)
brew.def_skill("Performance", 5)
brew.def_skill("Persuasion", 5)
brew.def_skill("Religion", 4)
brew.def_skill("Sleight of Hand", 2)
brew.def_skill("Stealth", 1)
brew.def_skill("Survival", 4)
