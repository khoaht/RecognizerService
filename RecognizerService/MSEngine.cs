﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using micautLib;
//using MSINKAUTLib;

namespace RecognizerService                                                     {
    public class MSEngine : IDisposable                                         {
        private AutomationElement _mipAutomationElement;
        private micautLib.MathInputControl _mipControl;
        private string _result;
        private bool inProgress = false;

        public MSEngine()                                                       {}

        public void StartMIP()                                                  {
            if (_mipControl == null)                                            {
                _mipControl = new micautLib.MathInputControl();
                var centerOfScreen = Screen.AllScreens[0].WorkingArea.Center();
                _mipControl.CenterOn(centerOfScreen);
                _mipControl.EnableExtendedButtons(true);
                _mipControl.EnableAutoGrow(true);
                _mipControl.Close += Application.ExitThread;
                _mipControl.Insert += OnInsert;

                _mipControl.SetCaptionText(Constants.ServiceName);
                _mipControl.Show();
                _mipControl.EnableExtendedButtons(true);
                _mipAutomationElement 
                    = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty,
                        Constants.ServiceName));                                }}

        public void EndMIP()                                                    {
            Dispose();                                                          }

        /// <summary>
        /// Hook onInsert method
        /// </summary>
        /// <param name="xml"></param>
        protected void OnInsert(string xml)                                     {
            try                                                                 {
                var text = xml;
                _result = Util.MathMlToLatex(xml);                              }
            catch (Exception e)                                                 {
                Console.WriteLine(e.Message);                                   }}


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()                                                   {
            if (_mipControl != null)                                            {
                _mipControl.Hide();
                _mipControl = null;                                             }}


        /// <summary>
        /// Get Latex result
        /// </summary>
        public string Result                                                    {
            get                                                                 {
                return _result;                                                 }
        }

        /// <summary>
        /// Get control with Button type
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public AutomationElement GetButton(string buttonName)                   {
            Condition conditions = new AndCondition(
                  new PropertyCondition(AutomationElement.IsEnabledProperty,
                      true),
                  new PropertyCondition(AutomationElement.ControlTypeProperty,
                      ControlType.Button), 
                      new PropertyCondition(AutomationElement.NameProperty,
                buttonName));

            AutomationElement buttonElement = 
                AutomationElement.RootElement.FindFirst(TreeScope.Descendants, 
                new PropertyCondition(AutomationElement.NameProperty, 
                    buttonName));
            return buttonElement;                                               }

        public AutomationElement GetControlItem(AutomationElement element, 
            ControlType controType, 
            string buttonName)                                                  {
            Condition conditions =
                new AndCondition(
                  new PropertyCondition(AutomationElement.IsEnabledProperty,
                      true),
                  new PropertyCondition(AutomationElement.ControlTypeProperty,
                     controType), 
                     new PropertyCondition(AutomationElement.NameProperty,
                buttonName));

            AutomationElement buttonElement = 
                AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                conditions);
            return buttonElement;                                               }


        /// <summary>
        /// Automation Invoker
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public InvokePattern GetInvokePattern(AutomationElement element)        {
            if (element != null && inProgress)                                  {
                lock (element)                                                  {
                    var pt = element.GetCurrentPattern(InvokePattern.Pattern) 
                        as InvokePattern;
                    Thread.Sleep(50);
                    inProgress = false;
                    return pt;                                                  }}
            return default(InvokePattern);                                      }


        /// <summary>
        /// Automation Invoke control
        /// </summary>
        /// <param name="text"></param>
        public void InvokeControl(string text)                                  {
            var button = GetButton(text);
            bool bInvoke = (bool)button
                .GetCurrentPropertyValue(
                AutomationElement.IsInvokePatternAvailableProperty);
            if (bInvoke)                                                        {
                InvokePattern click =
                    button.GetCurrentPattern(InvokePattern.Pattern)
                    as InvokePattern;
                try                                                             {
                    click.Invoke();                                             }
                catch (Exception e)                                             {                    
                    Thread.Sleep(100);
                    Console.WriteLine("Invoke error! : " + e.Message);          }}}

        public void InvokeControl(AutomationElement element,
            ControlType type, string controlText)                               {
            var button = GetControlItem(element, type, controlText);
            bool bInvoke = 
                (bool)button.GetCurrentPropertyValue(
                AutomationElement.IsInvokePatternAvailableProperty);
            if (bInvoke)                                                        {
                InvokePattern click = 
                    button.GetCurrentPattern(InvokePattern.Pattern)
                    as InvokePattern;
                try                                                             {
                    click.Invoke();                                             }
                catch (Exception e)                                             {                    
                    Thread.Sleep(100);
                    Console.WriteLine("Invoke error! : " + e.Message);          }}}



        /// <summary>
        /// Invoke MIP control
        /// </summary>
        /// <param name="points"></param>
        public string SendToMIP(int[] points)                                   {
            lock (_mipControl)                                                  {
                var TheInk = new MSINKAUTLib.InkDisp();
                var obj = TheInk.CreateStroke(points, null);
                var iink = (micautLib.IInkDisp)TheInk;
                _mipControl.LoadInk(iink);                                      }

            return Result;                                                      }

        /// <summary>
        /// Invoke MIP control
        /// </summary>
        /// <param name="points"></param>
        public string SendToMIP(IList<int[]> points)                            {
            lock (_mipControl)                                                  {
                var TheInk = new MSINKAUTLib.InkDisp();
                foreach (int[] item in points)                                  {
                    TheInk.CreateStroke(item, null);                            }

                var iink = (micautLib.IInkDisp)TheInk;
                _mipControl.LoadInk(iink);                                      }

            return Result;                                                      }}}
