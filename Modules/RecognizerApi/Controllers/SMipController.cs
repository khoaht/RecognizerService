using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using RecognizerApi.Models;
using RecognizerService;

namespace RecognizerApi.Controllers                                             {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SMipController : ApiController                                 {
        private RecognizerService.IRecognizerService service;
        public SMipController(RecognizerService.IRecognizerService service)     {
            this.service = service;                                             }

        /// <summary>
        /// handle dropping of the mouse events
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string request)                          {
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject(request, 
                typeof(List<StrokeData>)) as List<StrokeData>;
            var latex = service.Submit(list);
            var res = new RecognitionResults()                                  {
                instanceIDs = 0,
                Result = new Result()                                           {
                    certainty = string.Empty,
                    symbol =RemoveLatexCharacters(latex)                        }};

            return Request.CreateResponse<RecognitionResults>(HttpStatusCode.OK, 
                res, Configuration.Formatters.XmlFormatter);                    }

        private string RemoveLatexCharacters(string latex)                      {
            var result = latex.Replace("$", string.Empty);
            result = result.Replace("\\", string.Empty);
            result = result.Replace("{", string.Empty);
            result = result.Replace("}", string.Empty);
            return result;                                                      }}}
