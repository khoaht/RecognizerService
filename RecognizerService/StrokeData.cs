using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecognizerService
{
    [Serializable]
    public class StrokeData
    {
        public string Points { get; set; }

        public long TranslateX { get; set; }

        public long TranslateY { get; set; }
    }
}