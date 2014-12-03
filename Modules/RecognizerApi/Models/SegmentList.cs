using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace RecognizerApi.Models
{
    public class SegmentList                                                    {

        [XmlAttribute("maxima")]
        public string maxima { get; set; }
        [XmlAttribute("variable")]
        public string variable { get; set; }
        public Segment[] Segment { get; set; }
        [XmlAttribute("guid")]
        public string guid { get; set; }

        #region For OUTPUT
        [XmlAttribute("TexString")]
        public string TexString { get; set; }
        #endregion
                                                                                }

    public class Segment                                                        {

        #region For Input
        [XmlAttribute("type")]
        public string type { get; set; }
        [XmlAttribute("instanceID")]
        public int instanceID { get; set; }
        [XmlAttribute("scale")]
        public string scale { get; set; }
        [XmlAttribute("translation")]
        public string translation { get; set; }
        [XmlAttribute("points")]
        public string points { get; set; }
        [XmlAttribute("symbol")]
        public string symbol { get; set; }
        #endregion                                                              
                                                                                }}