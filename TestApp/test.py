
def Case_test1():
    Sugoi.Wait('computer.bmp');
    Sugoi.DoubleClick('computer.bmp')
    Sugoi.Wait('urlbar.bmp')
    Sugoi.Say(ImgPattern('urlbar.bmp').SetOffset(210,22),'d:\\')
    Assert.IsTrue(Sugoi.Exists('dflag.bmp'),'go to d drive fail')