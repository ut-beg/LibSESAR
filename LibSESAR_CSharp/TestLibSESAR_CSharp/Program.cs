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
            LibSESAR_CSharp.Models.SesarSample sample1 = new LibSESAR_CSharp.Models.SesarSample();
            LibSESAR_CSharp.Models.SesarSample sample2 = new LibSESAR_CSharp.Models.SesarSample();

            sample1.UserCode = "IEBEG";
            sample1.Material = "Rock";
            sample1.SampleType = "Core";
            sample1.IsPrivate = false;
            sample1.IGSN = "IEBEG00001";

            sample2.UserCode = "IEBEG";
            sample2.Material = "Rock";
            sample2.SampleType = "Core";
            sample2.IsPrivate = false;
            sample2.IGSN = "IEBEG00002";

            LibSESAR_CSharp.Models.SesarSamples samples = new LibSESAR_CSharp.Models.SesarSamples();
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(samples.GetType());
            
            samples.Samples.Add(sample1);
            samples.Samples.Add(sample2);

            x.Serialize(Console.Out, samples);

            System.Console.ReadLine();
        }
    }
}
