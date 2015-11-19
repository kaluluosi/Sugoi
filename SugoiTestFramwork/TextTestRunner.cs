using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    public class TextTestRunner : TestRunner {

        public TestResult Result { get; private set; }

        public override void Run(TestCase testCase) {
            Result = new TestResult();
            testCase.Run(Result);
            PrintResult();
        }

        public void PrintResult() {
            Console.WriteLine("fails" + Result.Failures.Count);
            Console.WriteLine("errors" + Result.Errors.Count);
            foreach (KeyValuePair<TestMethod, Exception> kv in Result.Errors) {
                Console.WriteLine(kv.Key.Name);
                Console.WriteLine(kv.Value.Message);
            }
        }
    }
}
