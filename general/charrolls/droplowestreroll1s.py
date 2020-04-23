
def l_roll():
	output = 1
	while (output == 1):
		output = roll(6)
	return output

def rollstat():
	rset = [l_roll(), l_roll(), l_roll(), l_roll()]
	return (sum(rset) - min(rset))
