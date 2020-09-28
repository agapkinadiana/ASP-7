using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab3.Models
{
    [DataContract]
    public class Link
    {
        [DataMember]
        public string Url { get; private set; }
        [DataMember]
        public string Method { get; private set; }

        public Link(string url, string method)
        {
            Url = url;
            Method = method;
        }
    }
}
