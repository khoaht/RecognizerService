﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Modules;
using RecognizerService;

namespace MathRecognizer
{
    public class LatexService : ILatexService
    {
        private RecognizerService.IRecognizerService service;

        public LatexService(RecognizerService.IRecognizerService service)
        {
            this.service = service;
        }
 
        /// <summary>
        /// Get Single latex
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public string GetSingleLatex(int[] points)
        {
            try
            {
                var result = service.Submit(points);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return string.Empty;
        }

        public string TestLatex()
        {
            return string.Format("$x$");
        }

    }
}