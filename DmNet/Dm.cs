﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dm;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace DmNet {
    /// <summary>
    /// 大漠对象单例工厂
    /// </summary>
    public class Dm {
        private const string dllDefaultPath = "./dm.dll";

        private static dmsoft dm;

        public static bool IsRegisted {
            get {
                Type t = Type.GetTypeFromCLSID(new Guid("{26037A0E-7CBD-4FFF-9C63-56F2D0770214}"));
                return t != null ? true : false;
            }
        }

        /// <summary>
        /// 默认dm对象
        /// </summary>
        public static dmsoft Default {
            get {
                //auto regist
                try {
                    if (IsRegisted == false)
                        RegistDM();
                }
                catch (FileNotFoundException ex) {
                    throw ex;
                }
                return dm == null ? dm = new dmsoft() : dm;
            }
        }


        public static void RegistDM(string path = dllDefaultPath) {
            if (File.Exists(dllDefaultPath)==false) {
                throw new FileNotFoundException(dllDefaultPath + " is not found. Make sure the dll is in the root path.");
            }
            string strCmd = string.Format("regsvr32 {0}", path);
            try {
                Process myProcess = new Process();
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("cmd.exe");
                myProcessStartInfo.Verb = "runas";
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.CreateNoWindow = true;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo = myProcessStartInfo;
                myProcessStartInfo.Arguments = "/s " + strCmd;
                myProcess.Start();
            }
            catch (System.Exception ex) {
                throw ex;
            }
        }


    }
}
