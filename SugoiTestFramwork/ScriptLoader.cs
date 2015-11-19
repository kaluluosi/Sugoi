﻿using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    /// <summary>
    /// 测试辅助工具
    /// </summary>
    public class ScriptLoader {
        public readonly static ScriptEngine engine = Python.CreateEngine();
        public readonly static Sugoi sugoi = new Sugoi();
        public readonly static SugoiTest assert = new SugoiTest();

        private static ScriptScope scope;
        public static ScriptScope Scope {
            get {
                if (scope == null) {
                    scope = engine.CreateScope();
                    scope.SetVariable("Sugoi", sugoi);
                    scope.SetVariable("Assert", assert);
                    scope.SetVariable("ImgPattern", typeof(ImgPattern));
                }
                return scope;
            }
        }


        /// <summary>
        /// 加载测试脚本并返回测试用例对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static TestCase LoadTestScript(string path) {
            path = Path.GetFullPath(path);
            Sugoi.SetScriptPath(Path.GetDirectoryName(path) + Path.DirectorySeparatorChar);

            ScriptSource script = engine.CreateScriptSourceFromFile(path);
            script.Execute(Scope);
            Action setUpClass;
            Action tearDownClass;
            Action setUp;
            Action tearDown;
            Action cleanUp;
            try {
                //找出setup和teardown
                setUpClass = Scope.GetVariable<Action>("SetUpClass");
                tearDownClass = Scope.GetVariable<Action>("TearDownClass");
            }
            catch (System.Exception ex) {
                setUpClass = tearDownClass = null;
            }

            try {
                //找出setup和teardown
                setUp = scope.GetVariable<Action>("SetUp");
                tearDown = scope.GetVariable<Action>("TearDown");
            }
            catch (System.Exception ex) {
                setUp = tearDown = null;
            }

            try {
                //找出clean up
                cleanUp = scope.GetVariable<Action>("CleanUp");
            }catch(System.Exception ex) {
                cleanUp = null;
            }
            

            //读取所有的变量名
            //过滤出所有名字以Case_开头的方法
            var testMethodNames = Scope.GetVariableNames();
            var testMethods = from tcn in testMethodNames
                             where tcn.ToLower().StartsWith("test_")
                             select new TestMethod(tcn,scope.GetVariable<Action>(tcn));

            return new TestCase(setUpClass, tearDownClass, setUp, tearDown, cleanUp, testMethods.ToList());
        }

    }
}
