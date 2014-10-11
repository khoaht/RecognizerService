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
        private static micautLib.MathInputControl _mipControl;
        private string _result;

        public MSEngine()
        {
                _mipControl = new micautLib.MathInputControl();

                _mipControl.Close += Application.ExitThread;
                _mipControl.Insert += OnInsert;
                _mipControl.SetCaptionText(Constants.ServiceName);

                //var ink = new MSINKAUTLib.InkDisp();
                //ink.Load(System.IO.File.ReadAllBytes("E:\\inkData.isf"));

                //var iink = (micautLib.IInkDisp)ink;
                _mipControl.Show();
                //_mipControl.LoadInk(iink);


                _mipAutomationElement = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Math Input Panel"));
           }



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

        public void Dispose()
        {
            if (_mipProcess != null)
            {
                _mipProcess.CloseMainWindow();
                _mipProcess.Dispose();
            }
        }

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

        public AutomationElement GetDigitButton(int number)
        {
            if ((number < 0) || (number > 9))
            {
                throw new InvalidOperationException("mumber must be a digit 0-9");
            }

            AutomationElement buttonElement = _mipAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, number.ToString()));

            if (buttonElement == null)
            {
                throw new InvalidOperationException("Could not find button corresponding to digit" + number);
            }

            return buttonElement;
        }

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

        public InvokePattern GetInvokePattern(AutomationElement element)
        {
            if (element != null)
                return element.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            return default(InvokePattern);
        }


        public void GetSample(string text)
        {
            GetInvokePattern(GetButton(text)).Invoke();
            _mipControl = null;
        }

        public void Reload(int[] points)
        {
            var TheInk = new MSINKAUTLib.InkDisp();
            var obj = TheInk.CreateStroke(points, null);
            var iink = (micautLib.IInkDisp)TheInk;
            _mipControl.LoadInk(iink);

            GetSample("Insert");
        }
    }
}
