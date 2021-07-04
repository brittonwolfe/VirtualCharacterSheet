
from VCS.IO import File, FileLoad

def load(path: str):
	return FileLoad.GetDataFile(path)

def dir(path: str):
	return FileLoad.DataDirectory().GetSubdir(sub);
