using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using RecognizerApi.Models;


namespace Infrastructure                                                        {
    public class RecognitionSerializer : XmlObjectSerializer                    {
        XmlSerializer serializer;
        public RecognitionSerializer()                                          {
            this.serializer = new XmlSerializer(typeof(RecognitionResults));    }

        public override void WriteObject(XmlDictionaryWriter writer, 
            object graph)                                                       {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, graph, ns);                            }

        public override bool IsStartObject(XmlDictionaryReader reader)          {
            throw new NotImplementedException();                                }

        public override object ReadObject(XmlDictionaryReader reader, 
            bool verifyObjectName)                                              {
            throw new NotImplementedException();                                }

        public override void WriteEndObject(XmlDictionaryWriter writer)         {
            throw new NotImplementedException();                                }

        public override void WriteObjectContent(XmlDictionaryWriter writer,
            object graph)                                                       {
            throw new NotImplementedException();                                }

        public override void WriteStartObject(XmlDictionaryWriter writer,
            object graph)                                                       {
            throw new NotImplementedException();                                }}


    public class AlignResponseSerializer : XmlObjectSerializer                  {
        XmlSerializer serializer;
        public AlignResponseSerializer()                                        {
            this.serializer = new XmlSerializer(typeof(AlignResponse));         }

        public override void WriteObject(XmlDictionaryWriter writer, 
            object graph)                                                       {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, graph, ns);                            }

        public override bool IsStartObject(XmlDictionaryReader reader)          {
            throw new NotImplementedException();                                }

        public override object ReadObject(XmlDictionaryReader reader, 
            bool verifyObjectName)                                              {
            throw new NotImplementedException();                                }

        public override void WriteEndObject(XmlDictionaryWriter writer)         {
            throw new NotImplementedException();                                }

        public override void WriteObjectContent(XmlDictionaryWriter writer, 
            object graph)                                                       {
            throw new NotImplementedException();                                }

        public override void WriteStartObject(XmlDictionaryWriter writer, 
            object graph)                                                       {
            throw new NotImplementedException();                                }}}
