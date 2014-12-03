﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        /// <summary>
        /// handle dropping of the mouse events
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>


        public HttpResponseMessage Get(string request)
        {
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject(request, typeof(List<StrokeData>)) as List<StrokeData>;
            var latex = service.Submit(list);

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

            res.SegmentList[0] = new SegmentList
                {
                    TexString = latex,
                    guid = Guid.NewGuid().ToString(),
                    variable = string.Empty
                };

            return Request.CreateResponse<AlignResponse>(HttpStatusCode.OK, res, Configuration.Formatters.XmlFormatter);
        }

        [HttpPost]
        public HttpResponseMessage Post(StrokeData[] request)
        {
            var latex = service.Submit(request.ToList());

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

            res.SegmentList[0] = new SegmentList
            {
                TexString = latex,
                guid = Guid.NewGuid().ToString(),
                variable = string.Empty
            };

            return Request.CreateResponse<AlignResponse>(HttpStatusCode.OK, res, Configuration.Formatters.XmlFormatter);
        }
    }
}
