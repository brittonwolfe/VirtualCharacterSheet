#todo
# The basic outline of how I want brew-API integration to work is:
#	- the init script (this file) will add the views through a method
#	- the method will copy the files to a working directory
#	- the files will recieve hooks to the temporary file set to be deleted
# This will all happen on brew load, and there will be a networking shell in
# core/net.py that you can run from the main shell.
core = _brew("core_5e")

core.AddPage('charsheet.cshtml')