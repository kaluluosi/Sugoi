using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class SugoiTest
    {
        public void IsTrue(bool condition) {
            if(condition == false) {
                throw new TestFailedException();
            }
        }

        public void IsTrue(bool condition, string message) {
            if (condition == false) {
                throw new TestFailedException(message);
            }
        }

        public void IsTrue(bool condition, string message,params object[] parameters) {
            if (condition == false) {
                throw new TestFailedException(string.Format(message,parameters));
            }
        }

        public void IsFalse(bool condition) {
            if(condition == true) {
                throw new TestFailedException();
            }
        }

        public void IsFalse(bool condition, string message) {
            if(condition == true) {
                throw new TestFailedException(message);
            }
        }

        public void IsFalse(bool condition, string message, params object[] parameters) {
            if(condition == true) {
                throw new TestFailedException(string.Format(message, parameters));
            }
        }

    }
}
