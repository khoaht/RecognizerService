using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecognizerService                                                     {
    public class RecognizerService : IRecognizerService                         {
        private readonly MSEngine msEngine;

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, 
            string lpWindowName);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public RecognizerService(MSEngine msEngine)                             {
            this.msEngine = msEngine;                                           }

        public string Submit(int[] points)                                      {
            string result = string.Empty;
            msEngine.StartMIP();
            result = msEngine.SendToMIP(points);
            while (string.IsNullOrEmpty(result))                                {
                CallBack();
                Thread.Sleep(100);
                result = msEngine.Result;                                       }
            msEngine.EndMIP();
            return result;                                                      }


        public string Result                                                    {
            get                                                                 {
                return msEngine.Result;                                         }}

        private void OnCallBack()                                               {
            msEngine.InvokeControl("Insert");                                   }

        public void CallBack()                                                  {
            OnCallBack();                                                       }


        public void End()                                                       {
            msEngine.EndMIP();                                                  }

        public string Submit(IList<StrokeData> strokes)                         {
            string result = string.Empty;
            msEngine.StartMIP();
            List<int[]> points = new List<int[]>();
            foreach (var item in strokes)                                       {
                var pnts = GetArray(item.Points, 
                    item.TranslateX, item.TranslateY);
                points.Add(pnts);
                                                                                }
            result = msEngine.SendToMIP(points);
            while (string.IsNullOrEmpty(result))                                {
                CallBack();
                Thread.Sleep(100);
                result = msEngine.Result;                                       }
            msEngine.EndMIP();

            return result;                                                      }

        public int[] GetArray(int[] points, long deltaX, long deltaY)           {

            int length = points.Length;
            int[] pnts = new int[length];

            for (int i = 0; i < length; i++)                                    {
                if (i % 2 == 0)
                    pnts[i] = points[i] + (int)deltaX;
                else
                    pnts[i] = points[i] + (int)deltaY;                          }
            return pnts;                                                        }

        public int[] GetArray(string points, long deltaX, long deltaY)          {

            var arrs = points.Split(',');
            int length = arrs.Length;
            int[] pnts = new int[length];

            for (int i = 0; i < length; i++)                                    {
                long p = 
                    long.Parse(string.IsNullOrEmpty(arrs[i]) ? "0" : arrs[i]);
                pnts[i] = 
                    (i % 2 == 0) ? (int)(p + deltaX) : (int)(p + deltaY); }
            return pnts;                                                        }}}
