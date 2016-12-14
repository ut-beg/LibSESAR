using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//This is a an application for testing LibSESAR_CSharp.  Not to be confused with unit tests, which I'll have to write later, I guess.

namespace TestLibSESAR_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                System.Console.WriteLine("Enter Seed: ");
                string seedString = System.Console.ReadLine();

                System.Console.WriteLine("Enter record count: ");
                string countString = System.Console.ReadLine();

                System.Console.WriteLine("Enter username: ");
                string username = System.Console.ReadLine();

                System.Console.WriteLine("Enter password: ");
                string password = System.Console.ReadLine();

                System.Console.WriteLine("Enter user code: ");
                string UserCode = System.Console.ReadLine();
                
                int seed = 0;
                int count = 1;

                if(Int32.TryParse(seedString, out seed) && Int32.TryParse(countString, out count))
                {
                    

                    LibSESAR_CSharp.Models.samples samps = new LibSESAR_CSharp.Models.samples();

                    List<LibSESAR_CSharp.Models.samplesSample> samples = new List<LibSESAR_CSharp.Models.samplesSample>();

                    for (int i = seed; i < seed + count; i++)
                    {
                        LibSESAR_CSharp.Models.samplesSample sample1 = new LibSESAR_CSharp.Models.samplesSample();
                        sample1.user_code = UserCode;
                        sample1.name = "Q" + i.ToString("D5");
                        sample1.sample_type = LibSESAR_CSharp.Models.sample_type.Toothpick;
                        sample1.material = LibSESAR_CSharp.Models.material.Rock;
                        sample1.is_private = LibSESAR_CSharp.Models.is_private.Item1;

                        samples.Add(sample1);
                    }

                    samps.sample = samples.ToArray();

                    LibSESAR_CSharp.SESARSampleSubmissionRequest sssr = new LibSESAR_CSharp.SESARSampleSubmissionRequest();
                    sssr.ServiceEndpoint = LibSESAR_CSharp.Constants.SESAREndpointProductionUrlBase;
                    sssr.Samples = samps;

                    sssr.UserName = username;
                    sssr.Password = password;

                    await sssr.DoRequest();

                    //Validate the results
                    if (sssr.RequestResult != null)
                    {
                       
                        LibSESAR_CSharp.Models.SampleSubmissionResponse resp = sssr.RequestResultModel;

                        if(resp != null)
                        {
                            if (resp.StatusCode == 200)
                            {
                                Console.WriteLine("Success.");

                                for (int i = 0; i <resp.SampleList.Count; i++)
                                {
                                    //Do something with the results.
                                    Console.WriteLine("Name: " + resp.SampleList[i].Name + " IGSN: " + resp.SampleList[i].IGSN);
                                }
                            }
                            else if(resp.StatusCode == 400) //An error
                            {
                                //XMl Syntax error
                                if(resp.Valid != null && resp.Valid.Value == "no" && resp.Valid.Code == "InvalidXML")
                                {
                                    Console.WriteLine("XML Syntax Error.  Report the bug...");
                                }
                                else if(resp.SampleList != null && resp.SampleList.Count > 0) //Business data out of bounds kind of error
                                {
                                    for(int i=0; i < resp.SampleList.Count; i++)
                                    {
                                        Console.WriteLine("Sample data error: " + resp.SampleList[i].Error);
                                    }
                                }
                            }
                            else if(resp.StatusCode == 401)
                            {
                                Console.WriteLine("Incorrect username or password.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Deserialization of the return failed.  Shouldn't happen unless SESAR changes the schema...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Http request failed specatacularly - reason unknown");
                    }

                    System.Console.ReadLine();
                }

                
            }).Wait();
        }
    }
}
