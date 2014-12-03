using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace RecognizerApi.Models                                                  {
    [XmlRoot(ElementName = "RecognitionResults", 
        Namespace = "", DataType = "Test")]
    public class RecognitionResults                                             {
        [XmlAttribute("instanceIDs")]
        public int instanceIDs { get; set; }
        [XmlElement("Result")]
        public Result Result { get; set; }
                                                                                }

    public class Result                                                         {
        [XmlAttribute("symbol")]
        public string symbol { get; set; }
        [XmlAttribute("certainty")]
        public string certainty { get; set; }                                   }


    [XmlRoot(ElementName = "AlignResponse", Namespace = "")]
    public class AlignResponse                                                  {
        [XmlAttribute("result")]
        public string result { get; set; }
        [XmlAttribute("error")]
        public string error { get; set; }
        [XmlElement("exerciseStep")]
        public exerciseStep exerciseStep { get; set; }
        [XmlElement("SegmentList")]
        public SegmentList[] SegmentList { get; set; }                          }
    
    public class exerciseStep                                                   {
        [XmlAttribute("message")]
        public string message { get; set; }
        [XmlAttribute("istrue")]
        public bool istrue { get; set; }
        [XmlAttribute("isfinish")]
        public bool isfinish { get; set; }
                                                                                }}