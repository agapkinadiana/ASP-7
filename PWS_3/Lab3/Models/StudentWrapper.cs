using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab3.Models
{
    [DataContract]
    public class StudentWrapper<T>
    {
        [DataMember]
        public T Data { get; private set; }
        [DataMember]
        public LinkWrapper Links { get; private set; }

        public StudentWrapper(T data, LinkWrapper links)
        {
            Data = data;
            Links = links;
        }
    }
}
