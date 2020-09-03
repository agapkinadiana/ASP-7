using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace PWS_1
{
    public class WebHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public static int result = 0;

        public static Stack<int> stack = new Stack<int>();
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpResponse res = context.Response;
            HttpRequest req = context.Request;
            res.ContentType = "application/json";

            switch (req.HttpMethod)
            {
                case "GET":
                    {
                        try
                        {
                            int top = stack.Peek();
                            res.Write(js.Serialize(new { result = result + stack.FirstOrDefault(), stack }));
                        }
                        catch (InvalidOperationException)
                        {
                            res.Write(js.Serialize(new { result, stack = "Stack is empty" }));
                        }
                    };
                    break;
                case "POST":
                    {
                        int number;

                        if (int.TryParse(req.Params["result"], out number))
                        {
                            try
                            {
                                result = number;
                                int top = stack.Peek();
                                res.Write(js.Serialize(new { result = result + top, stack }));
                            }
                            catch (InvalidOperationException)
                            {
                                res.Write(js.Serialize(new { result, stack = "Stack is empty" }));
                            }
                        }
                        else
                        {
                            res.Write(js.Serialize(new { error = new { message = "Type of Params['result'] is not Integer", result = req.Params["result"] } }));
                        }
                    };
                    break;
                case "PUT":
                    {
                        int number;

                        if (int.TryParse(req.Params["add"], out number))
                        {
                            stack.Push(number);
                            try
                            {
                                int top = stack.Peek();
                                res.Write(js.Serialize(new { result = result + top, stack }));
                            }
                            catch (InvalidOperationException)
                            {
                                res.Write(js.Serialize(new { result, stack = "Stack is empty" }));
                            }
                        }
                        else
                        {
                            res.Write(js.Serialize(new { error = new { message = "Type of Params['add'] is not Integer", result = req.Params["add"] } }));
                        }
                    };
                    break;
                case "DELETE":
                    {
                        try
                        {
                            stack.Pop();
                            int top = stack.Peek();
                            res.Write(js.Serialize(new { result = result + top, stack }));
                        }
                        catch (InvalidOperationException)
                        {
                            res.Write(js.Serialize(new { result, stack = "Stack is empty" }));
                        };
                    };
                    break;
            }
        }

        #endregion
    }
}
