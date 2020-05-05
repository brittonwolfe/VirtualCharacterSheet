
id = new_s("Acid Splash")
_s(id).Description = "You hurl a bubble of acid. Choose one creature within range, or two creatures within range that are within five feet of each other. A target must succeed on a Dexterity saving throw or take 1D6 acid damage."
_s(id).Vocal = True
_s(id).Somatic = True
_s(id).Material = False
_s(id).Range = 60

def Behavior(caster, target):
    if(not target.DexSave(caster.SaveDC())):
        target.Damage(roll(6))

set_pyf("core_5e:Acid_Splash.Behavior", Behavior)
