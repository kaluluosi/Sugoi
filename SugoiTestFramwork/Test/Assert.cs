using SugoiTestFramework.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test
{
    public class Assert
    {
        public static void IsTrue(bool condition) {
            if(condition == false) {
                throw new TestFailedException();
            }
        }

        public static void IsTrue(bool condition, string message) {
            if (condition == false) {
                throw new TestFailedException(message);
            }
        }

        public static void IsTrue(bool condition, string message,params object[] parameters) {
            if (condition == false) {
                throw new TestFailedException(string.Format(message,parameters));
            }
        }

        public static void IsFalse(bool condition) {
            if(condition == true) {
                throw new TestFailedException();
            }
        }

        public static void IsFalse(bool condition, string message) {
            if(condition == true) {
                throw new TestFailedException(message);
            }
        }

        public static void IsFalse(bool condition, string message, params object[] parameters) {
            if(condition == true) {
                throw new TestFailedException(string.Format(message, parameters));
            }
        }
 
        public static void IsExisted(Region r ,PatternBase pattern) {
            IsTrue(r.Find(pattern) != null, "{0} is not existed!", pattern.ToString());
        }

        public static void IsNotExisted(Region r,PatternBase pattern) {
            IsTrue(r.Find(pattern) == null, "{0} is existed!", pattern.ToString());
        }
    }
}
