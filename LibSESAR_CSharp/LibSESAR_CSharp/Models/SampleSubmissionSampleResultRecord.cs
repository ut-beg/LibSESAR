using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibSESAR_CSharp.Models
{
    //[XmlElement(ElementName="sample")]
    public class SampleSubmissionSampleResultRecord
    {
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "igsn")]
        public string IGSN { get; set; }

        //[XmlAttribute(AttributeName = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "valid")]
        public SampleSubmissionValidRecord Valid { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }
}
