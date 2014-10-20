using System;
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

        public string TestLatex(string id)
        {
            return string.Format("$x$");
        }


        /// <summary>
        /// Sample:
        /*
        segmentList=%3CSegmentList%3E%3CSegment%20type=%22pen_stroke%22%20instanceID=%222%22%20scale=%221,1%22%20translation=%22352,42%22%20points=%220,5|0,5|0,5|0,4|0,3|1,2|6,0|9,0|13,0|16,0|20,0|22,0|25,0|27,0|28,0|28,2|28,5|28,8|24,9|21,11|18,13|16,13|14,15|12,15|13,15|15,17|17,17|19,18|20,18|21,19|23,20|23,21|23,22|23,24|23,26|23,27|23,29|23,30|23,32|22,35|19,37|17,38|16,40|14,41|12,43|11,44|9,45|7,46%22/%3E%3C/SegmentList%3E&segment=false
        Response Headersview source

         */
        /// </summary>
        /// <param name="segmentList"></param>
        /// <returns></returns>
        public string GetLatex(string strokes)
        {
            int startStroke = strokes.IndexOf("points=") + 8;
            string sStroke = strokes.Substring(startStroke);
            string bStroke = sStroke.Substring(0, sStroke.IndexOf("\""));
            throw new NotImplementedException();
        }
    }
}