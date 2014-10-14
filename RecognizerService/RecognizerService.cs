using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecognizerService
{
    public class RecognizerService : IRecognizerService
    {
        private readonly MSEngine msEngine;

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public RecognizerService(MSEngine msEngine)
        {
            this.msEngine = msEngine;
        }

        public string Submit(int[] points)
        {
            string result = string.Empty;

            msEngine.StartMIP();
            result =  msEngine.SendToMIP(points);
            msEngine.EndMIP();
            return result;
        }


        public string Result
        {
            get { return msEngine.Result; }
        }

        private void OnCallBack()
        {

            msEngine.InvokeControl("Insert");
        }

        public void CallBack()
        {
            OnCallBack();
        }


        public void End()
        {
            msEngine.EndMIP();
        }
    }
}
