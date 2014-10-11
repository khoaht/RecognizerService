using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using micautLib;
//using MSINKAUTLib;

namespace RecognizerService
{
    public class MSEngine : IDisposable
    {
        private Process _mipProcess;
        private AutomationElement _mipAutomationElement;
        private AutomationElement _resultTextBoxAutomationElement;
        private micautLib.MathInputControl _mipControl;
        private string _result;

        public MSEngine()
        {

        }

        public void StartMIP()
        {
            if (_mipControl == null)
            {
                _mipControl = new micautLib.MathInputControl();

                _mipControl.Close += Application.ExitThread;
                _mipControl.Insert += OnInsert;
                _mipControl.SetCaptionText(Constants.ServiceName);

                //var ink = new MSINKAUTLib.InkDisp();
                //ink.Load(System.IO.File.ReadAllBytes("E:\\inkData.isf"));
                //var iink = (micautLib.IInkDisp)ink;     
                //_mipControl.LoadInk(iink);

                _mipControl.Show();
                _mipAutomationElement = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, Constants.ServiceName));
            }

            
        }

        public void EndMIP()
        {
            ///_mipControl.Hide();
            //InvokeControl("Close");

            //this.Dispose();

        }
        /// <summary>
        /// Hook onInsert method
        /// </summary>
        /// <param name="xml"></param>
        protected void OnInsert(string xml)
        {
            try
            {
                var text = xml;
                _result = Util.MathMlToLatex(xml);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_mipProcess != null)
            {
                _mipProcess.CloseMainWindow();
                _mipProcess.Dispose();
            }
        }


        /// <summary>
        /// Get Latex result
        /// </summary>
        public string Result
        {
            get
            {
                return _result;
            }

            //get
            //{
            //    return _resultTextBoxAutomationElement.GetCurrentPropertyValue(AutomationElement.NameProperty);
            //}
            //set
            //{
            //    string stringRep = value.ToString();

            //    for (int index = 0; index < stringRep.Length; index++)
            //    {
            //        int leftDigit = int.Parse(stringRep[index].ToString());

            //        GetInvokePattern(GetDigitButton(leftDigit)).Invoke();
            //    }
            //}
        }

        /// <summary>
        /// Get control with Button type
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public AutomationElement GetButton(string buttonName)
        {
            Condition conditions = new AndCondition(
                  new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                  new PropertyCondition(AutomationElement.ControlTypeProperty,
                      ControlType.Button)
                , new PropertyCondition(AutomationElement.NameProperty,
                buttonName)

                  );

            //var buttonElement = _mipAutomationElement.FindFirst(TreeScope.Descendants, conditions);
            AutomationElement buttonElement = AutomationElement.RootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, buttonName));
            return buttonElement;

        }


        /// <summary>
        /// Automation Invoker
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public InvokePattern GetInvokePattern(AutomationElement element)
        {
            if (element != null)
                return element.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            return default(InvokePattern);
        }

        /// <summary>
        /// Automation Invoke control
        /// </summary>
        /// <param name="text"></param>
        public void InvokeControl(string text)
        {
            GetInvokePattern(GetButton(text)).Invoke();
        }


        /// <summary>
        /// Invoke MIP control
        /// </summary>
        /// <param name="points"></param>
        public void SendToMIP(int[] points)
        {
            var TheInk = new MSINKAUTLib.InkDisp();
            var obj = TheInk.CreateStroke(points, null);
            var iink = (micautLib.IInkDisp)TheInk;
            _mipControl.LoadInk(iink);

            InvokeControl("Insert");
        }
    }
}
