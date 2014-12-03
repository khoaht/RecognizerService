using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace RecognizerApi.Models
{
    [XmlRoot(ElementName = "AlignResponse", Namespace = "", DataType = "Test")]
    public class AlignResponse
    {

        [XmlElement("maxima")]
        public maxima maxima { get; set; }

        [XmlElement("SegmentList")]
        public SegmentList SegmentList { get; set; }
    }

    public class maxima
    {
        [XmlAttribute("variable")]
        public string variable { get; set; }

       [XmlText]
        public string text { get; set; }
    }

    //public class SegmentList
    //{
    //    [XmlAttribute("TexString")]
    //    public string TexString { get; set; }

    //    [XmlAttribute("variable")]
    //    public string variable { get; set; }
    //}

}