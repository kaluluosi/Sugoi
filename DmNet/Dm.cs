using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmNet;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace DmNet
{
    /// <summary>
    /// 大漠对象单例工厂
    /// </summary>
    public class Dm
    {
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
                    if(IsRegisted == false)
                        RegistDM();
                }
                catch(FileNotFoundException ex) {
                    throw ex;
                }
                return dm == null ? dm = new dmsoft() : dm;
            }
        }

        [DllImport(dllDefaultPath)]
        public static extern int DllRegisterServer();
        
        [DllImport(dllDefaultPath)]
        public static extern int DllUnregisterServer();

        /// <summary>
        /// 静默注册，注册失败会抛出异常
        /// </summary>
        public static void RegistDM() {
            int ret = DllRegisterServer();
            if(ret < 0) {
                throw new RegistFailException();
            }
        }

        /// <summary>
        /// 静默卸载
        /// </summary>
        public static void UnregistDM() {
            DllUnregisterServer();
        }


        /// <summary>
        /// 命令行注册，需要管理员权限
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showMsgBox"></param>
        public static void RegistDMByCmd(string path = dllDefaultPath,bool showMsgBox=false) {
            if(File.Exists(dllDefaultPath) == false) {
                throw new FileNotFoundException(dllDefaultPath + " is not found. Make sure the dll is in the root path.");
            }
            string strCmd = string.Format("regsvr32 {0}", path);
            string msgbox = showMsgBox ? "/c " : "/s ";

            Process myProcess = new Process();
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("cmd.exe");
            myProcessStartInfo.Verb = "runas";
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo = myProcessStartInfo;
            myProcessStartInfo.Arguments = msgbox + strCmd;
            myProcess.Start();
        }

        /// <summary>
        /// 命令行卸载，需要管理员权限
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showMsgBox"></param>
        public static void UnregistDMByCmd(string path = dllDefaultPath,bool showMsgBox=false) {
            if(File.Exists(dllDefaultPath) == false) {
                throw new FileNotFoundException(dllDefaultPath + " is not found. Make sure the dll is in the root path.");
            }

            string strCmd = string.Format("regsvr32 /u {0}", path);
            string msgbox = showMsgBox ? "/c " : "/s ";

            Process myProcess = new Process();
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("cmd.exe");
            myProcessStartInfo.Verb = "runas";
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo = myProcessStartInfo;
            myProcessStartInfo.Arguments = msgbox + strCmd;
            myProcessStartInfo.RedirectStandardError = true;
            myProcess.Start();
            string error = myProcess.StandardError.ReadToEnd();
            if(string.IsNullOrEmpty(error) == false) {
                throw new Exception(error);
            }
        }


    }
}
