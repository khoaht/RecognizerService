using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecognizerService                                                     {    
    /// <summary>
    /// Stroke Data
    /// </summary>
    public class StrokeData                                                     {
        public int InstanceId { get; set; }
        public string Points   { get; set; }

        public long TranslateX { get; set; }

        public long TranslateY { get; set; }                                    }}