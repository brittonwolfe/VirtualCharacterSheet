
potion = def_i('core_5e:potion_of_healing')
potion.Name = "Potion of Healing"

def use(event):
	event.Target.Meta.Health += (2 + rolln(2, 4))

potion.AddBehavior('use', use)
