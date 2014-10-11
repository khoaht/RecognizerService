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
        public string DoSample()
        {
            msEngine.GetSample("Insert");
            return msEngine.Result;
        }

        public void  Submit(int[] points)
        {
            msEngine.Reload(points);
        }


        public string Result
        {
            get { return msEngine.Result; }
        }

        private void OnCallBack()
        {
            msEngine.GetSample("Insert"); 
        }

        public void CallBack()
        {
            OnCallBack();
        }
    }
}
