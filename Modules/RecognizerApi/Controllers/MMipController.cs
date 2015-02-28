using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using RecognizerApi.Models;
using RecognizerService;

namespace RecognizerApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MMipController : ApiController
    {
        private RecognizerService.IRecognizerService service;

        public MMipController(RecognizerService.IRecognizerService service)
        {
            this.service = service;
        }

        private string RemoveLatexCharacters(string latex)
        {
            var result = latex.Replace("$", string.Empty);
            result = result.Replace("\\", string.Empty);
            result = result.Replace("{", string.Empty);
            result = result.Replace("}", string.Empty);
            return result;
        }

        /// <summary>
        /// handle dropping of the mouse events
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(string request)
        {
            var res = new RecognitionResults()
            {
                instanceIDs = 0,
                Result = new Result()
                {
                    certainty = string.Empty,
                    symbol = string.Empty
                }
            };
            if (!string.IsNullOrEmpty(request))
            {
                var list =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(request,
                    typeof(List<StrokeData>)) as List<StrokeData>;
                var latex = service.Submit(list);

                res.instanceIDs = list.First().InstanceId;
                res.Result.symbol = RemoveLatexCharacters(latex).Trim();
            }

            return Request.CreateResponse<RecognitionResults>(HttpStatusCode.OK,
                                    res, Configuration.Formatters.XmlFormatter);
        }

        [HttpPost]
        public HttpResponseMessage Post(StrokeData[][] request)
        {
            var res = new AlignResponse()
            {
                result = "0",
                error = string.Empty,
                exerciseStep = new exerciseStep()
                {
                    isfinish = false,
                    istrue = true,
                    message = string.Empty
                },
                SegmentList = new SegmentList[1]
            };

            var latexs = string.Empty;
            if (request != null && request.Length > 0)
            {

                for (int i = 0; i < request.Length; i++)
                {
                    var item = request[i];
                    var latex = service.Submit(item.ToList());
                    latexs += latex;
                    Thread.Sleep(100);
                }


                res.SegmentList[0] = new SegmentList
                {
                    TexString = latexs,
                    guid = Guid.NewGuid().ToString(),
                    variable = string.Empty
                };
            }

            return Request.CreateResponse<AlignResponse>(HttpStatusCode.OK,
                res, Configuration.Formatters.XmlFormatter);
        }


        //[HttpPost]
        //public HttpResponseMessage Post(StrokeData[] request)                   {
        //    var res = new AlignResponse()                                       {
        //        result = "0",
        //        error = string.Empty,
        //        exerciseStep = new exerciseStep()                               {
        //            isfinish = false,
        //            istrue = true,
        //            message = string.Empty                                      },
        //        SegmentList = new SegmentList[1]                                };

        //    if (request != null && request.Length > 0)                          {
        //        var latex = service.Submit(request.ToList());              

        //        res.SegmentList[0] = new SegmentList                            {
        //            TexString = latex,
        //            guid = Guid.NewGuid().ToString(),
        //            variable = string.Empty                                     };}

        //    return Request.CreateResponse<AlignResponse>(HttpStatusCode.OK,
        //        res, Configuration.Formatters.XmlFormatter);                    }

    }
}


