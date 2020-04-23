
def rollstat():
	rset = [roll(5) + 1, roll(5) + 1, roll(5) + 1, roll(5) + 1]
	return (sum(rset) - min(rset))
