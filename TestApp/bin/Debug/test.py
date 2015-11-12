
import unittest


def setUp(self):
    print "test start"
def tearDown(self):
    print "test stop"
def testSzBig(self):
    num = 10
    assert num==10
   
def testSzSmall(self):
    num = -10
    assert num==-10
   
def testSzEqual(self):
    num = 0
    assert num==1
 


