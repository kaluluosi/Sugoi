using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Test
{
    public class Assert
    {
        public void AssertTrue(bool condition) {
            if(condition == false) {
                throw new TestFailedException();
            }
        }

        public void AssertTrue(bool condition, string message) {
            if (condition == false) {
                throw new TestFailedException(message);
            }
        }

        public void AssertTrue(bool condition, string message,params object[] parameters) {
            if (condition == false) {
                throw new TestFailedException(string.Format(message,parameters));
            }
        }

        public void AssertFalse(bool condition) {
            if(condition == true) {
                throw new TestFailedException();
            }
        }

        public void AssertFalse(bool condition, string message) {
            if(condition == true) {
                throw new TestFailedException(message);
            }
        }

        public void AssertFalse(bool condition, string message, params object[] parameters) {
            if(condition == true) {
                throw new TestFailedException(string.Format(message, parameters));
            }
        }

    }
}
