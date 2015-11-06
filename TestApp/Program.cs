using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet;
using DmNet.Windows;

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            a a1 = new a("a1");
            a b1 = new a("b1");
            a b2 = new a("b2");

            a1.Set(ref b1);
            b1.Set(ref a1.obj); //理论上b1的obj = B1
            a1.Set(ref b2);//理论上这时候b1的obj =b2

            Console.Read();
        }

        class a
        {
            private string name;
            public a obj;
            public a(string name) {
                this.name = name;
            }
            public void Set(ref a obj) {
                this.obj = obj;
            }
        }
    }
}
