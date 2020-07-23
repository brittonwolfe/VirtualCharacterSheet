clr.AddReference('vcs')
from VirtualCharacterSheet.IO import File

def save_object(obj, file):
	objtype = typeof(obj)
	if not hasattr(objtype, "SerializableAttribute"):
		print(f'{objtype} is not serializable!')
		return false
	if typeof(file) is str:
		save_object(obj, File(file))
	return objtype.Save(obj, file.GetBinaryWriter())

def load_object(type, file):
	pass
