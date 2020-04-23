import math
import random

def roll():
	output = 1
	while (output == 1):
		output = math.floor(random.random() * 6) + 1
	return output

def rollstat():
	rset = [roll(), roll(), roll(), roll()]
	return (sum(rset) - min(rset))
