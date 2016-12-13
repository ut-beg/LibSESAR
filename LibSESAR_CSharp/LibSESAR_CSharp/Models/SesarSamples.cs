using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibSESAR_CSharp.Models
{
    [XmlRoot(ElementName = "samples", Namespace = "http://app.geosamples.org")]
    public class SesarSamples
    {
        [XmlArray("samples"), XmlArrayItem(typeof(SesarSample), ElementName = "sample") ]
        public List<SesarSample> Samples = new List<SesarSample>();
    }
}
