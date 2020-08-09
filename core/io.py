clr.AddReference('vcs')
from os.path import expanduser
from VirtualCharacterSheet.IO import File

def save_object(obj: object, file: File):
	objtype = type(obj)
	if not hasattr(objtype, 'Serialize'):
		print(objtype, 'is not seraializable!')
		return False
	if type(file) is str:
		return save_object(obj, File(expanduser(file)))
	return objtype.Serialize(obj, file.GetBinaryWriter())

def load_object(objtype: type, file: File):
	if not hasattr(objtype, 'Deserialize'):
		print(objtype, 'is not serializable!')
		return False
	if type(file) is str:
		return load_object(objtype, File(expanduser(file)))
	return objtype.Deserialize(file.GetBinaryReader())
