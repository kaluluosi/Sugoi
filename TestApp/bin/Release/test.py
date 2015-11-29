
def Test_method1():
    
    Sugoi.Wait('computer.bmp')
    Sugoi.DoubleClick('computer.bmp')
    Sugoi.AssertExist('flag.bmp',"没找到flag.bmp")