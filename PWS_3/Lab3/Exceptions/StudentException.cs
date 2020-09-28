using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Exceptions
{
    public class StudentException : Exception
    {
        public int Code { get; private set; }

        public StudentException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
