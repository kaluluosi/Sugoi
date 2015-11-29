using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test {
    public class TestResult { 


        /// <summary>
        /// 测试记录集合
        /// </summary>
        private Dictionary<TestMethod, TestFailedException> failures = new Dictionary<TestMethod, TestFailedException>();
        private List<TestMethod> successes = new List<TestMethod>();
        private Dictionary<TestMethod, Exception> errors = new Dictionary<TestMethod, Exception>();

        public Dictionary<TestMethod, TestFailedException> Failures {
            get {
                return failures;
            }

            set {
                failures = value;
            }
        }

        public List<TestMethod> Successes {
            get {
                return successes;
            }

            set {
                successes = value;
            }
        }

        public Dictionary<TestMethod, Exception> Errors {
            get {
                return errors;
            }

            set {
                errors = value;
            }
        }

        public void AddFailure(TestMethod testMethod, TestFailedException failureError) {
            if (Failures.ContainsKey(testMethod))
                Failures[testMethod] = failureError;
            else
                Failures.Add(testMethod, failureError);
        }

        public void AddSuccess(TestMethod testMethod) {
            if (Successes.Contains(testMethod)) return;
            Successes.Add(testMethod);
        }

        public void AddError(TestMethod testMethod, Exception error) {
            if (Errors.ContainsKey(testMethod))
                Errors[testMethod] = error;
            else
                Errors.Add(testMethod, error);
        }

        public bool wasSuccessed() {
            return failures.Count == 0;
        }


    }
}
