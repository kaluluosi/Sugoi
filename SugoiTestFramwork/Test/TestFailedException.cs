﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test {
    public class TestFailedException : Exception {
        public TestFailedException(string msg) : base(msg) {

        }

        public TestFailedException() {
            
        }
    }
}
