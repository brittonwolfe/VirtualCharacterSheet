import math
import random

def roll():
    return math.floor(random.random() * 6) + 1

def rollstat():
    rset = [roll(), roll(), roll(), roll()]
    return (sum(rset) - min(rset))
