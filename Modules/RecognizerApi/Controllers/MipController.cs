using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;
using System.Xml.Serialization;
using RecognizerApi.Models;
using RecognizerService;

namespace RecognizerApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MipController : ApiController
    {
        private RecognizerService.IRecognizerService service;

        public MipController(RecognizerService.IRecognizerService service)
        {
            this.service = service;
        }
        // GET api/GetFraz/5
        public string GetFraz(string points)
        {
            //Sample:"<RecognitionResults instanceIDs="4"><Result symbol="3" certainty =""/></RecognitionResults>

            try
            {
                var arrs = points.Split(',');
                int length = arrs.Length;
                int[] pnts = new int[length];
                for (int i = 0; i < length; i++)
                {
                    pnts[i] = int.Parse(string.IsNullOrEmpty(arrs[i]) ? "0" : arrs[i]);
                }
                var result = service.Submit(pnts);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return string.Empty;
        }


        /// <summary>
        /// handle dropping of the mouse events
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage GetSymbol(string request)
        {
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject(request, typeof(List<StrokeData>)) as List<StrokeData>;
            var latex = service.Submit(list);
            var res = new RecognitionResults()
            {
                instanceIDs = 0,

                Result = new Result()
                {
                    certainty = string.Empty,
                    symbol = list.Count == 1 ? RemoveLatexCharacters(latex) : latex
                }
            };


            return Request.CreateResponse<RecognitionResults>(HttpStatusCode.OK, res, Configuration.Formatters.XmlFormatter);
        }




        //public HttpResponseMessage GetSymbol(string request)
        //{
        //    var list = Newtonsoft.Json.JsonConvert.DeserializeObject(request, typeof(List<StrokeData>)) as List<StrokeData>;

        //    var res = new RecognitionResults()
        //    {
        //        instanceIDs = 0,
        //        Result = new Result()
        //        {
        //            certainty = string.Empty,
        //            symbol = service.Submit(list)
        //        }
        //    };


        //    return Request.CreateResponse<RecognitionResults>(HttpStatusCode.OK, res, Configuration.Formatters.XmlFormatter);
        //}

        private string RemoveLatexCharacters(string latex)
        {
            //$\\frac{}{}$
            var result = latex.Replace("$", string.Empty);
            result = result.Replace("\\", string.Empty);
            result = result.Replace("{", string.Empty);
            result = result.Replace("}", string.Empty);
            return result;
        }

        private SegmentList ParseXMl(string xml)
        {
            SegmentList result = new SegmentList();
            result.Segment = new Segment[1];
            var seg = new Segment();
            seg.type = "pen_stroke";


            if (!String.IsNullOrEmpty(xml))
            {
                XDocument doc = XDocument.Parse(xml);
                if (doc != null && doc.Root != null)
                {
                    XElement root = doc.Root;
                    //parse Result
                    XElement eleResult = root.Element(XName.Get("Segment"));

                    if (eleResult != null)
                    {
                        seg.type = (string)eleResult.Attribute("type");
                        seg.instanceID = (int)eleResult.Attribute("instanceID");
                        seg.scale = (string)eleResult.Attribute("scale");
                        seg.points = (string)eleResult.Attribute("points");
                    }
                }
            }

            result.Segment[0] = seg;

            return result;
        }

    }


}
