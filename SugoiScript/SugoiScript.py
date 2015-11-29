import clr,sys
clr.AddReferenceToFile("dmnet.dll")

from dmnet import *

s = dm.Create()

print(s.Ver())

input()