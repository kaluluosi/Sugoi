import keys

def test_opencomputer():
    keys.printHelloWorld()
    Sugoi.Wait('computer.bmp');
    Sugoi.DoubleClick('computer.bmp')
    Sugoi.Click('ddrive.bmp')
    Assert.IsTrue(Sugoi.Exists('dflag.bmp'),'go to d drive fail')
