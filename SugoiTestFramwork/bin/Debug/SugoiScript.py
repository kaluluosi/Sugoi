import clr,sys
sys.path.append(r'./')
clr.AddReferenceToFile("SugoiTestFramework.dll")

from SugoiTestFramework import *

s = Pattern.Screen.Instance

print(s)

input()