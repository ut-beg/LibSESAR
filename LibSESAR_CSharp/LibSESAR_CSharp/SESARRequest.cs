using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LibSESAR_CSharp
{
    public abstract class SESARRequest
    {
        /// <summary>
        /// The endpoint to which the request will be submitted.  Defaults to the test instance.
        /// </summary>
        public string ServiceEndpoint { get; set; } = Constants.SESAREndpointTestingUrlBase;

        /// <summary>
        /// This is the relative path at the endpoint for command.
        /// </summary>
        public abstract string RelativePath { get; }

        /// <summary>
        /// Returns whether or not the request will require authentication.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetRequireAuthentication()
        {
            return false;
        }

        /// <summary>
        /// The username to be used in cases where the request requires authentication.
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// The password to be used in cases where the request requires authentication.
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        
        /// <summary>
        /// The full path to the XML service to be requested.
        /// </summary>
        public string FullPath
        {
            get
            {
                return ServiceEndpoint + RelativePath;
            }
        }

        private object _requestResult = null;
        
        public object RequestResult
        {
            get
            {
                return _requestResult;
            }

            private set
            {
                _requestResult = value;
            }

        }

        public HttpStatusCode StatusCode
        {
            get;
            set;

        } = HttpStatusCode.Unused;

        /// <summary>
        /// Generates whatever is required in the request body.
        /// </summary>
        /// <returns>string value of the request body.</returns>
        protected abstract string GenerateRequestBody();
        
        public async Task DoRequest()
        {
            Models.SampleSubmissionResponse ret = null;

            System.Net.HttpWebRequest wreq = (HttpWebRequest) System.Net.WebRequest.Create(FullPath);

            string reqBody = string.Empty;

            
            
            if (GetRequireAuthentication())
            {
                string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(UserName + ":" + Password));

                wreq.Headers.Add("Authorization", "Basic " + encoded);
                
                    

                //This service requires the user to submit the username and password, in addition to the BASIC authentication token
                reqBody += WebUtility.UrlEncode("username") + "=" + WebUtility.UrlEncode(UserName) + "&";
                reqBody += WebUtility.UrlEncode("password") + "=" + WebUtility.UrlEncode(Password) + "&";
            }

            reqBody += GenerateRequestBody();

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bodyBytes = encoding.GetBytes(reqBody);
            wreq.ContentType = "application/x-www-form-urlencoded";
            wreq.ContentLength = bodyBytes.Length;
            
            //Add the actual response body.
            wreq.Method = "POST";
            //Set some of the headers.
            wreq.Accept = "text/xml, application/xml";

            //Acquire the body stream, and write the body into it.
            System.IO.Stream reqStream = await wreq.GetRequestStreamAsync();
            reqStream.Write(bodyBytes, 0, bodyBytes.Length);

            HttpWebResponse hwresp = null;
            
            try
            {
                WebResponse wresp = await wreq.GetResponseAsync();
                hwresp = (HttpWebResponse)wresp;

                WebHeaderCollection whc = hwresp.Headers;

                StatusCode = hwresp.StatusCode;
            }
            catch (WebException ex)
            {
                hwresp = (HttpWebResponse)ex.Response;
            }
            
            System.Xml.Serialization.XmlSerializer xd = new System.Xml.Serialization.XmlSerializer(typeof(Models.SampleSubmissionResponse));

            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(hwresp.GetResponseStream());

                RequestResult = await sr.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                //Ret stays null here.  Bad news.
            }
        }
    }
}
