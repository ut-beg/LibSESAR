using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LibSESAR_CSharp
{    
    public class SESARSampleSubmissionRequest : SESARRequest
    {
        public override string RelativePath
        {
            get
            {
                return "upload.php";
            }
        }

        public override bool GetRequireAuthentication()
        {
            return true;
        }

        /// <summary>
        /// The set of samples to submit.
        /// </summary>
        public Models.samples Samples { get; set; }

        public Models.SampleSubmissionResponse RequestResultModel
        {
            get
            {
                Models.SampleSubmissionResponse ret = null;

                if(RequestResult != null)
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Models.SampleSubmissionResponse));

                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
                    writer.Write(RequestResult);
                    writer.Flush();
                    stream.Position = 0;

                    ret = (Models.SampleSubmissionResponse) xs.Deserialize(stream);
                    ret.StatusCode = StatusCode;
                }

                return ret;
            }
        }

        protected override string GenerateRequestBody()
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Models.samples));

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            xs.Serialize(ms, Samples);
            ms.Position = 0;
            System.IO.StreamReader sr = new System.IO.StreamReader(ms);

            string content = sr.ReadToEnd();

            System.Console.Write(content);

            string ret = WebUtility.UrlEncode("content") + "=" + WebUtility.UrlEncode(content);

            return ret;
        }

    }
}
