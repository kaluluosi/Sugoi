using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    public class TestCase {

        public TestCase(
                Action setUpClass,
                Action tearDownClass,
                Action setUp,
                Action tearDown,
                Action cleanUp,
                List<TestMethod> testMethods
            ) {
                TestMethods = new List<TestMethod>();
            SetUpClass = setUpClass;
            TearDownClass = tearDownClass;
            SetUp = setUp;
            TearDown = tearDown;
            CleanUp = cleanUp;
            TestMethods = testMethods;
        }

        public Action SetUpClass { get; private set; }
        public Action TearDownClass { get; private set; }
        public Action SetUp { get; private set; }
        public Action TearDown { get; private set; }
        public Action CleanUp { get; private set; }

        public List<TestMethod> TestMethods { get; private set; } 


        public TestResult Run(TestResult result) {

            try {
                if (result == null) result = new TestResult();

                if (SetUpClass != null)
                    SetUpClass();

                foreach (TestMethod method in TestMethods) {
                    try {
                        if(SetUp != null)
                            SetUp();
                        method.Invoke();
                        if(TearDown != null)
                            TearDown();
                        if (CleanUp != null)
                            CleanUp();
                        result.AddSuccess(method);
                    }
                    catch (TestFailedException tfe) {
                        result.AddFailure(method, tfe);
                        OnFailed(method);
                    }
                    catch (Exception e) {
                        result.AddError(method, e);
                        OnError(method);
                    }
                }

                if (TearDownClass != null)
                    TearDownClass();
            }
            catch (System.Exception ex) {
                result.AddError(new TestMethod("InitError", null), ex);
            }

            return result;

        }

        public event EventHandler<TestMethod> Failed;
        public event EventHandler<TestMethod> Error;

        public void OnFailed(TestMethod tm) {
            if(Failed != null)
                Failed(this, tm);
        }

        public void OnError(TestMethod tm) {
            if(Error != null)
                Error(this, tm);
        }
    }
}
