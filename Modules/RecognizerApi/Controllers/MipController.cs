using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
            try
            {
                var arrs = points.Split(',');
                int length=arrs.Length;
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



    }
}
