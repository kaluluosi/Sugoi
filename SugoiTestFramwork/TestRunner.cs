using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    public class TestRunner {
        public readonly static ScriptEngine engine = Python.CreateEngine();
        public readonly static ScriptScope scope = engine.CreateScope();

        public Dictionary<string, bool> Result {
            get {
                return result;
            }

            set {
                result = value;
            }
        }

        public Dictionary<string, string> Message {
            get {
                return message;
            }

            set {
                message = value;
            }
        }


        private ScriptSource script;
        /// <summary>
        /// key = case name,bool = result
        /// </summary>
        private Dictionary<string, bool> result = new Dictionary<string, bool>();
        /// <summary>
        /// key = case name,string = message
        /// </summary>
        private Dictionary<string, string> message = new Dictionary<string, string>();
        private List<string> testCases = new List<string>();


        public Action SetUp;
        public Action TearDown;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        static TestRunner() {
            Sugoi sugoi = new Sugoi();
            SugoiTest assert = new SugoiTest();
            scope.SetVariable("Sugoi", sugoi);
            scope.SetVariable("Assert", assert);
        }



        public void LoadTestScript(string path) {
            script = engine.CreateScriptSourceFromFile(path);
            script.Execute(scope);
            try {
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
                result.Add(caseName, true);
                message.Add(caseName, "Pass");
            }
            catch (TestFailedException ex) {
                result.Add(caseName, false);
                message.Add(caseName, ex.Message);
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
