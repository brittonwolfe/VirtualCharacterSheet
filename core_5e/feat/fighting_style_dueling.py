
def behavior():
	if local.This.Equipped.Length == 1:
		return 2

feat = def_feat("core_5e:fighting_style_dueling")
feat.Name = "Fighting Style: Dueling"
feat.Behavior = behavior
