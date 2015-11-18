using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.Windows
{
    public class InvalidHandleException : Exception
    {
        public int Hwnd { get; set; }

        public InvalidHandleException(int hwnd, string message)
            : base(message) {
                Hwnd = hwnd;

        }
    }
}
