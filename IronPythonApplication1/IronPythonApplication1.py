import clr
import sys
clr.AddReference('SugoiTestFramwork.dll')
clr.AddReference('DmNet.dll')

from SugoiTestFramwork import *
from DmNet import *

sugoi = Sugoi()
dm = Dm.Default