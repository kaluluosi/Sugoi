

def Case_test1():
    Sugoi.DoubleClick('computer.bmp')
    Sugoi.Wait(1)
    Assert.IsTrue(Sugoi.Exists('flag.bmp'),'computer not open')

def Case_test2():
    Sugoi.AppWin.Mouse.MoveTo(500,500)
    Assert.IsTrue(True,'fail')