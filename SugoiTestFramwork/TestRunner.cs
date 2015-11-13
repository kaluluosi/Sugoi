using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    public class TestRunner {
        public readonly static ScriptEngine engine = Python.CreateEngine();
        public readonly static ScriptScope scope = engine.CreateScope();
        public readonly static Sugoi sugoi = new Sugoi();
        public readonly static SugoiTest assert = new SugoiTest();

        private ScriptSource script;
        /// <summary>
        /// 测试用例方法集合
        /// </summary>
        private List<string> testCases = new List<string>();
        /// <summary>
        /// 测试记录集合
        /// </summary>
        private Dictionary<string, string> failures = new Dictionary<string, string>();
        private List<string> successes = new List<string>();
        private Dictionary<string, string> errors = new Dictionary<string, string>();


        public Action SetUp;
        public Action TearDown;

        public List<string> TestCases {
            get {
                return testCases;
            }
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        static TestRunner() {
            scope.SetVariable("Sugoi", sugoi);
            scope.SetVariable("Assert", assert);
            scope.SetVariable("ImgPattern", typeof(ImgPattern));
        }

        public void AddFailure(string testCase,string errorMsg) {
            if (failures.ContainsKey(testCase))
                failures[testCase] = errorMsg;
            else
                failures.Add(testCase, errorMsg);
        }

        public void AddSuccess(string testCase) {
            if (successes.Contains(testCase)) return;
            successes.Add(testCase);
        }

        public void AddError(string testCase,string errorMsg) {
            if (errors.ContainsKey(testCase))
                errors[testCase] = errorMsg;
            else
                errors.Add(testCase, errorMsg);
        }

        public bool wasSuccessed(string testCase) {
            return successes.Contains(testCase);
        }

        public void LoadTestScript(string path) {
            Sugoi.SetImgPath(Path.GetDirectoryName(path)+Path.DirectorySeparatorChar);
            Console.WriteLine(Sugoi.ImgPath);
            script = engine.CreateScriptSourceFromFile(path);
            script.Execute(scope);
            try {
                //找出setup和teardown
                SetUp = scope.GetVariable<Action>("SetUp");
                TearDown = scope.GetVariable<Action>("TearDown");
            }
            catch (System.Exception ex) {
                SetUp = TearDown = null;
            }
            //读取所有的变量名
            testCases = scope.GetVariableNames().ToList();
            //过滤出所有名字以Case_开头的方法
            testCases = testCases.FindAll(c => c.StartsWith("Case_"));
        }

        public void Run(string caseName) {
            if (testCases.Contains(caseName) == false) return;

            try {
                Action testMethod = scope.GetVariable<Action>(caseName);
                testMethod();
                AddSuccess(caseName);
            }
            catch (TestFailedException tfe) {
                AddFailure(caseName, tfe.Message);
            }
            catch(Exception e) {
                AddError(caseName, e.Message);
            }
            
        }

        public void RunAll() {
            if (SetUp != null)
                SetUp();

            foreach (string caseName in testCases) {
                Run(caseName);
            }

            if (TearDown != null)
                TearDown();
        }

    }
}
