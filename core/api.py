
from VCS.Net.Api import MiscController

class MiscItem:
	typename = None
	itemname = None
	def __init__(self, typename, itemname = None):
		self.typename = typename
		self.itemname = itemname
	def generate_response(self, **kwargs):
		pass
