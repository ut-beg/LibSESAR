using System;
using System.Collections.Generic
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibSESAR_CSharpTests
{
    [TestClass]
    public class SampleSubmissionTests
    {
        [TestMethod]
        public async void TestSuccessfulSubmit()
        {
            //Create the sample submission request
            LibSESAR_CSharp.SESARSampleSubmissionRequest request = new LibSESAR_CSharp.SESARSampleSubmissionRequest();
            
            //Defaults to the test instance - be sure to set this to the production instance in your app
            //request.ServiceEndpoint = LibSESAR_CSharp.Constants.SESAREndpointProductionUrlBase;

            //Create the sample "collection" object
            request.Samples = new LibSESAR_CSharp.Models.samples();

            //Set your user credentials
            request.UserName = "aaron@myorg.edu";
            request.Password = "notR3allymp455w0rd";            

            //Create and populate a sample
            LibSESAR_CSharp.Models.samplesSample sample = new LibSESAR_CSharp.Models.samplesSample();
            sample.name = "Q00001";
            sample.user_code = "IEBEG";
            sample.sample_type = LibSESAR_CSharp.Models.sample_type.Toothpick;
            sample.material = LibSESAR_CSharp.Models.material.Rock;
            sample.is_private = LibSESAR_CSharp.Models.is_private.Item1;
            //More fields here...

            //Add the sample(s) to the request.
            request.Samples.sample = new LibSESAR_CSharp.Models.samplesSample[1] { sample };

            //Execute the request against the web service
            await request.DoRequest();

            //Get the response object from our request object
            LibSESAR_CSharp.Models.SampleSubmissionResponse response = request.RequestResultModel;

            //We had a successful HTTP request, right?
            if(response == null || response.StatusCode != (int) System.Net.HttpStatusCode.OK)
            {
                Assert.Fail("Request did not succeed.");
            }

            if(response.SampleList.Count == 0)
            {
                Assert.Fail("Response has no sample records.  Did they change the service?");
            }

            //Success!  Access the IGSN as response.SampleList[0].IGSN;

        }
    }
}
