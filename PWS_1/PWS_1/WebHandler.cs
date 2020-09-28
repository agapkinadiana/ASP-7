using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace PWS_1
{
    public class WebHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public static int result = 0;
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            switch (request.HttpMethod)
            {
                case "GET":
                    {
                        if (context.Session["stack"] == null)
                        {
                            context.Session["stack"] = new Stack<int>();
                        }
                        Stack<int> stack = (Stack<int>)context.Session["stack"];
                        string resultStack = "";
                        foreach (var i in stack.ToArray())
                        {
                            resultStack += i + " ";
                        }

                        int final = result;
                        if (stack.Count > 0)
                        {
                            final += stack.Peek();
                        }

                        response.Write($"Get: {final} ({result})" + ", stack: " + resultStack);
                    };
                    break;
                case "POST":
                    {
                        int resultPost = result + int.Parse(request.Params["result"]);
                        result = resultPost;
                        
                        context.Response.Write($"POST: {resultPost}");
                    };
                    break;
                case "PUT":
                    {
                        string add = request.Params["add"];

                        if (add == null)
                        {
                            add = "1";
                        }
                        //PUT
                        if (context.Session["stack"] != null)
                        {
                            Stack<int> stack = (Stack<int>)context.Session["stack"];
                            stack.Push(int.Parse(add));
                            context.Session["stack"] = stack;
                            string result = "";
                            foreach (var i in stack.ToArray())
                            {
                                result += i + " ";
                            }
                            context.Response.Write($"PUT: {result}");
                        }
                        else
                        {
                            response.Redirect("/");
                        }
                    };
                    break;
                case "DELETE":
                    {
                        if (context.Session["stack"] != null)
                        {
                            Stack<int> stack = (Stack<int>)context.Session["stack"];
                            stack.Pop();
                            context.Session["stack"] = stack;
                            string result = "";
                            foreach (var i in stack.ToArray())
                            {
                                result += i + " ";
                            }
                            context.Response.Write($"DELETE: {result}");
                        }
                        else
                        {
                            response.Redirect("/");
                        }
                    };
                    break;
            }
        }

        #endregion
    }
}