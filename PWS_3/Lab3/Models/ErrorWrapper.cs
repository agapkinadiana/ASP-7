using Lab3.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab3.Models
{
    [DataContract]
    public class ErrorWrapper
    {
        [DataMember]
        public int Code { get; private set; }
        [DataMember]
        public string Message { get; private set; }
        [DataMember]
        public Link Info { get; private set; }

        public ErrorWrapper(Exception error)
        {
            StudentException studentError = error as StudentException;
            if (studentError != null)
            {
                Code = studentError.Code;
                Info = new Link($"/resources/errors/{studentError.Code}", "GET");
            }
            
            Message = error.Message;
        }
    }
}
