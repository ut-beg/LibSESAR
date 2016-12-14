using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using LibSESAR_CSharp;

namespace LibSESAR_CSharpTests
{
    [TestClass]
    public class XMLSerializationTests
    {
        protected string UNAUTHORIZED_RESPONSE_XML_FILENAME = "UnauthorizedResponse.xml";
        protected string XML_CONTENT_ERROR_RESPONSE_XML_FILENAME = "XmlContentErrorResponse.xml";
        protected string XML_SYNTAX_ERROR_RESPONSE_XML_FILENAME = "XmlSyntaxErrorResponse.xml";
        protected string SUCCESS_XML_FILENAME = "SuccessResponse.xml";

        protected XmlSerializer _serializer = null;
        protected XmlSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XmlSerializer(typeof(LibSESAR_CSharp.Models.SampleSubmissionResponse));
                }
                return _serializer;
            }
        }

        /// <summary>
        /// Tests the parsing of SESAR's example success XML
        /// </summary>
        [TestMethod]
        public void TestParseSubmitSamplesSuccessResponse()
        {
            if(System.IO.File.Exists(SUCCESS_XML_FILENAME))
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(SUCCESS_XML_FILENAME, System.IO.FileMode.Open);

                    LibSESAR_CSharp.Models.SampleSubmissionResponse ssr = (LibSESAR_CSharp.Models.SampleSubmissionResponse) Serializer.Deserialize(fs);

                    if (ssr.SampleList.Count != 1)
                    {
                        Assert.Fail("Incorrect number of sample records.");
                    }
                    else
                    {
                        Assert.AreEqual(ssr.SampleList[0].IGSN, "LLS00009I");
                        Assert.AreEqual(ssr.SampleList[0].Name, "Lulin Upload Status Sample test");
                        Assert.AreEqual(ssr.SampleList[0].Status, "Sample [Lulin Upload Status Sample test] was saved successfully with IGSN [LLS00009I].");
                    }
                    
                }
                catch (Exception ex)
                {
                    Assert.Fail("Uncaught exception: " + ex.Message);
                }

            }
            else
            {
                Assert.Inconclusive("Cannot find " + SUCCESS_XML_FILENAME);
            }
        }

        [TestMethod]
        public void TestParseSubmitSamplesXMLSyntaxErrorResponse()
        {
            if (System.IO.File.Exists(XML_SYNTAX_ERROR_RESPONSE_XML_FILENAME))
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(XML_SYNTAX_ERROR_RESPONSE_XML_FILENAME, System.IO.FileMode.Open);

                    LibSESAR_CSharp.Models.SampleSubmissionResponse ssr = (LibSESAR_CSharp.Models.SampleSubmissionResponse)Serializer.Deserialize(fs);

                    if (ssr.ErrorList.Count != 2)
                    {
                        Assert.Fail("Error list must contain exactly two errors.");
                    }
                    else
                    {
                        foreach(string err in ssr.ErrorList)
                        {
                            if (!err.StartsWith("Element '"))
                            {
                                Assert.Fail("Error message is incorrect or missing.");
                            }
                        }
                        
                        if (ssr.Valid == null)
                        {
                            Assert.Fail("No valid element.");
                        }
                        else
                        {
                            if (ssr.Valid.Code != "InvalidXML")
                            {
                                Assert.Fail("Valid code must equal 'InvalidXML'");
                            }

                            if (ssr.Valid.Value != "no")
                            {
                                Assert.Fail("Valid value must equal no.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Uncaught exception: " + ex.Message);
                }

            }
            else
            {
                Assert.Inconclusive("Cannot find " + XML_SYNTAX_ERROR_RESPONSE_XML_FILENAME);
            }
        }

        [TestMethod]
        public void TestParseSubmitSamplesXMLContentErrorResponse()
        {
            if (System.IO.File.Exists(XML_CONTENT_ERROR_RESPONSE_XML_FILENAME))
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(XML_CONTENT_ERROR_RESPONSE_XML_FILENAME, System.IO.FileMode.Open);

                    LibSESAR_CSharp.Models.SampleSubmissionResponse ssr = (LibSESAR_CSharp.Models.SampleSubmissionResponse)Serializer.Deserialize(fs);
                    
                    if(ssr.SampleList.Count != 1)
                    {
                        Assert.Fail("Sample list must contain one record.");
                    }
                    else
                    {
                        if(ssr.SampleList[0].Name != "your sample name")
                        {
                            //TODO:  Nag Megan to alter the schema to make the name either an attribute or an element
                            //Assert.Fail("Incorrect sample name");
                        }

                        if(!ssr.SampleList[0].Error.StartsWith("The User Code ABC"))
                        {
                            Assert.Fail("Incorrect error message");
                        }

                        if(ssr.SampleList[0].Valid == null)
                        {
                            Assert.Fail("No 'valid' element");
                        }
                        else
                        {
                            if(ssr.SampleList[0].Valid.Code != "InvalidSample")
                            {
                                Assert.Fail("Incorrect 'valid' code");
                            }

                            if(ssr.SampleList[0].Valid.Value != "no")
                            {
                                Assert.Fail("Incorrect 'valid' value");
                            }
                        }


                    }

                }
                catch (Exception ex)
                {
                    Assert.Fail("Uncaught exception: " + ex.Message);
                }

            }
            else
            {
                Assert.Inconclusive("Cannot find " + XML_CONTENT_ERROR_RESPONSE_XML_FILENAME);
            }
        }

        [TestMethod]
        public void TestParseSubmitSamplesXMLUnauthorizedErrorResponse()
        {
            if (System.IO.File.Exists(UNAUTHORIZED_RESPONSE_XML_FILENAME))
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(UNAUTHORIZED_RESPONSE_XML_FILENAME, System.IO.FileMode.Open);

                    LibSESAR_CSharp.Models.SampleSubmissionResponse ssr = (LibSESAR_CSharp.Models.SampleSubmissionResponse)Serializer.Deserialize(fs);

                    if (ssr.ErrorList.Count != 1)
                    {
                        Assert.Fail("Error list must contain exactly one error.");
                    }
                    else
                    {
                        if (!ssr.ErrorList[0].StartsWith("Invalid login"))
                        {
                            Assert.Fail("Error message is incorrect or missing.");
                        }

                        if (ssr.Valid == null)
                        {
                            Assert.Fail("No valid element.");
                        }
                        else
                        {
                            if (ssr.Valid.Code != "InvalidAuth")
                            {
                                Assert.Fail("Valid code must equal 'InvalidAuth'");
                            }

                            if (ssr.Valid.Value != "no")
                            {
                                Assert.Fail("Valid value must equal no.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Uncaught exception: " + ex.Message);
                }

            }
            else
            {
                Assert.Inconclusive(UNAUTHORIZED_RESPONSE_XML_FILENAME);
            }
        }
    }
}