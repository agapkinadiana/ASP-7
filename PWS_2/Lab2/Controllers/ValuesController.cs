using Lab2.Models;
using System.Web;
using System.Web.Http;

namespace Lab2.Controllers
{
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return StackService.Get();
        }

        public string Post([FromUri]int result)
        {
            return StackService.Post(result);
        }

        public string Put([FromUri]int add)
        {
            return StackService.Put(add);
        }

        public string Delete()
        {
            string response;
            if (StackService.StackState() > 0)
            {
                response = StackService.Delete();
            }
            else
            {
                response = "The Stack is empty";
            }
            return response;
        }
    }
}
