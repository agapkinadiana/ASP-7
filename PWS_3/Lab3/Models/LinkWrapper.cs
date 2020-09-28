using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab3.Models
{
    [DataContract]
    public class LinkWrapper
    {
        [DataMember]
        public Link Self { get; private set; }
        [DataMember]
        public List<Link> Items { get; private set; }

        public LinkWrapper(Link self, List<Link> items)
        {
            Self = self;
            Items = items;
        }
    }
}
