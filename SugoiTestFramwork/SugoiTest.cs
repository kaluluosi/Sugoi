﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class SugoiTest
    {
        public void IsTrue(bool condition, string message) {
            if(condition == false) {
                throw new Exception(message);
            }
        }
    }
}