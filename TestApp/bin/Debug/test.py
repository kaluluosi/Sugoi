
def Test_method1():
    Sugoi.Wait('computer.bmp');
    Sugoi.DoubleClick('computer.bmp')
    Assert.IsTrue(Sugoi.Exists('flag.bmp'),'go to d drive fail')