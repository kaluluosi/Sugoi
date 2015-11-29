using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test {
    public class TextTestRunner : TestRunner {

        public TestResult Result { get; private set; }

        public override void Run(TestCase testCase) {
            Result = new TestResult();
            testCase.Run(Result);
            PrintResult();
        }

        public void PrintResult() {
            Console.WriteLine("failures：" + Result.Failures.Count);
            foreach(KeyValuePair<TestMethod,TestFailedException> kv in Result.Failures) {
                Console.WriteLine("{0} {1}", kv.Key.Name, kv.Value.Message);
            }

            Console.WriteLine("errors：" + Result.Errors.Count);
            foreach (KeyValuePair<TestMethod, Exception> kv in Result.Errors) {
                Console.WriteLine("{0} {1}", kv.Key.Name, kv.Value.Message);
            }
        }
    }
}
