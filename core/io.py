clr.AddReference('vcs')
from VirtualCharacterSheet.IO import File

def save_object(obj, file):
	objtype = type(obj)
	if not hasattr(objtype, 'Serialize'):
		print(objtype, 'is not seraializable!')
		return False
	if type(file) is str:
		return save_object(obj, File(file))
	return objtype.Serialize(obj, file.GetBinaryWriter())

def load_object(objtype, file):
	if not hasattr(objtype, 'Deserialize'):
		print(objtype, 'is not serializable!')
		return False
	if type(file) is str:
		return load_object(objtype, File(file))
	return objtype.Deserialize(file.GetBinaryReader())
