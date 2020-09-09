
from VirtualCharacterSheet.Util import _pyf, set_pyf, def_s, roll

spell = def_s("Core_5e:Acid Splash")
spell.Description = "You hurl a bubble of acid. Choose one creature within range, or two creatures within range that are within five feet of each other. A target must succeed on a Dexterity saving throw or take 1D6 acid damage."
spell.Book = "Player's Handbook"
spell.Pages = "211"
spell.Vocal = True
spell.Somatic = True
spell.Material = False
spell.Range = 60

def Behavior(caster, target):
	if(not target.DexSave(caster.SaveDC())):
		target.Damage(roll(6))

set_pyf("core_5e:Acid_Splash.Behavior", Behavior)

spell.SetBehavior(_pyf("core_5e:Acid_Splash.Behavior"))
