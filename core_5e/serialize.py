
from VCS import PlayerCharacter
from VCS.IO.Serialization import ScriptedObjectSet

def char_serialize(target, writer, shouldclose = True):
	if type(target) is not PlayerCharacter:
		return False
	writer.Write(target.Meta.MaxHealth)
	writer.Write(target.Meta.Health)
	if shouldclose:
		writer.Close()
	return True

def char_deserialize(reader, shouldclose = True):
	output = ScriptedObjectSet()
	output.Meta['MaxHealth'] = reader.ReadInt32()
	output.Meta['Health'] = reader.ReadInt32()
	if shouldclose:
		reader.Close()
	return output

PlayerCharacter.AddSerializationHook('core_5e', char_serialize, char_deserialize)
