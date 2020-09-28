using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public static class StackService
    {
        private static int result = 0;
        public static Stack<int> stack { get; private set; } = new Stack<int>();

        public static string Get()
        {
            int _result = result;
            if (stack.Count != 0)
            {
                _result += stack.Peek();
            }
            return JsonConvert.SerializeObject(new { RESULT = _result });
        }

        public static string Post(int _result)
        {
            result = _result;
            return Get();
        }

        public static string Put(int add)
        {
            stack.Push(add);
            return Get();
        }

        public static string Delete()
        {
            stack.Pop();
            return Get();
        }

        public static int StackState()
        {
            return stack.Count();
        }
    }
}