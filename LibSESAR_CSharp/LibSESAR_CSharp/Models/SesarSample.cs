﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibSESAR_CSharp.Models
{
    [XmlType(Namespace = "http://app.geosamples.org", TypeName ="sample")]
    public class SesarSample
    {
        /// <summary>
        /// The SESAR user code, typically five characters.  Ex: IEBEG
        /// </summary>
        [XmlElement("user_code")]
        public string UserCode { get; set; }

        /// <summary>
        /// This is the sample type, from the controlled list.  Ex:  Core, cuttings, etc.
        /// </summary>
        [XmlElement("sample_type")]
        public string SampleType { get; set; }

        /// <summary>
        /// This is the material type, from controlled list.  Ex: Rock, Mineral, Biology, etc.
        /// </summary>
        [XmlElement("material")]
        public string Material { get; set; }

        /// <summary>
        /// The IGSN of the sample.  When creating new samples, this will be automatically generated by the API if left blank.  Otherwise, it should start with the user code.
        /// </summary>
        [XmlElement("igsn")]
        public string IGSN { get; set; }

        /// <summary>
        /// The IGSN of the parent of the current sample.
        /// </summary>
        [XmlElement("parent_igsn")]
        public string ParentIGSN { get; set; }

        /// <summary>
        /// Whether or not the sample should be kept private 
        /// </summary>
        [XmlElement("is_private")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// The date upon which the sample can be made public
        /// </summary>
        [XmlElement("publish_date")]
        public DateTime PublishDate { get; set; }



    }
}