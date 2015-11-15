using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork {
    public class TestMethod {
        private Action method;

        public string Name { get;private set; }

        public TestMethod(string name,Action method) {
            this.Name = name;
            this.method = method;
        }

        public void Invoke() {
            if (method != null)
                method();
        }
    }
}
