using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LibSESAR_CSharp.Models
{
    [XmlRoot(ElementName = "results")]
    public class SampleSubmissionResponse
    {
       
        [XmlElement("sample")]
        public List<SampleSubmissionSampleResultRecord> SampleList { get; set; }

        [XmlElement("valid")]
        public SampleSubmissionValidRecord Valid { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("error")]
        public List<string> ErrorList { get; set; }

        [XmlIgnore]
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }
}
