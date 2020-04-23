
def rollstat():
    rset = [roll(6), roll(6), roll(6), roll(6)]
    return (sum(rset) - min(rset))
