using Lab3.Exceptions;
using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Lab3.Filters
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {   
            context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new ErrorWrapper(context.Exception));
        }
    }
}
