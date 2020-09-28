using Lab3.Exceptions;
using Lab3.Filters;
using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Lab3.Controllers
{
    public class StudentsController : ApiController
    {
        private const string API_PATH = "/api/students";

        private StudentsRepository Repository { get; set; } = new StudentsRepository();


        [HttpGet]
        [Exception]
        public StudentWrapper<List<Dictionary<string, object>>> Get(
            [FromUri] string likePattern = null,
            [FromUri] string globalLikePattern = null,
            [FromUri] int maxId = -1,
            [FromUri] int minId = 0,
            [FromUri] int limit = 10,
            [FromUri] int offset = 0,
            [FromUri] int sort = 0,
            [FromUri] string columns = "Id,Name,Phone"
            )
        {
            if (string.IsNullOrEmpty(columns) || !Regex.Match(columns, "^([A-Z][a-z]+(,|$))+$").Success)
            {
                throw new StudentException(101, "'columns' parameter must meet the pattern: 'Id,Name,Phone'");
            }

            string[] studentColumns = columns.Split(',');

            List<Dictionary<string, object>> filteredStudents = Repository.Get()
                .Where(st => string.IsNullOrEmpty(likePattern) ? true : st.Name.Contains(likePattern))
                .Where(st => string.IsNullOrEmpty(globalLikePattern) ? true : st.Id.ToString().Contains(globalLikePattern) || st.Name.Contains(globalLikePattern) || st.Phone.Contains(globalLikePattern))
                .Where(st => st.Id >= minId && (maxId == -1 || (maxId != -1 && st.Id <= maxId)))
                .Skip(offset).Take(limit)
                .OrderBy(st => sort == 0 ? st.Id.ToString() : st.Name).Select(st =>
                {
                    Dictionary<string, object> studentRecord = new Dictionary<string, object>();
                    foreach (string column in studentColumns)
                    {
                        PropertyInfo property = st.GetType().GetProperty(column);
                        if (property != null)
                        {
                            studentRecord[column] = property.GetValue(st);
                        }
                    }
                    return studentRecord;
                }).ToList();

            LinkWrapper linkWrapper = new LinkWrapper(new Link(API_PATH, "GET"), new List<Link>
            {
                new Link(API_PATH, "GET"),
                new Link($"{API_PATH}/<id>", "GET"),
                new Link(API_PATH, "PUT"),
                new Link(API_PATH, "POST"),
                new Link($"{API_PATH}/<id>", "DELETE")
            });
            return new StudentWrapper<List<Dictionary<string, object>>>(filteredStudents, linkWrapper);
        }

        [HttpGet]
        [Exception]
        public StudentWrapper<Student> Get([FromUri] int id)
        {
            Student student = Repository.Get(id);
            LinkWrapper linkWrapper = new LinkWrapper(new Link($"{API_PATH}/{id}", "GET"), new List<Link>
            {
                new Link(API_PATH, "GET"),
                new Link(API_PATH, "PUT"),
                new Link(API_PATH, "POST"),
                new Link($"{API_PATH}/{student.Id}", "DELETE")
            });
            return new StudentWrapper<Student>(student, linkWrapper);
        }

        [HttpPost]
        [Exception]
        public StudentWrapper<Student> Post(Student student)
        {
            Student createdStudent = Repository.Add(student);
            LinkWrapper linkWrapper = new LinkWrapper(new Link(API_PATH, "POST"), new List<Link>
            {
                new Link(API_PATH, "GET"),
                new Link($"{API_PATH}/{createdStudent.Id}", "GET"),
                new Link(API_PATH, "PUT"),
                new Link($"{API_PATH}/{createdStudent.Id}", "DELETE")
            });
            return new StudentWrapper<Student>(createdStudent, linkWrapper);
        }

        [HttpPut]
        [Exception]
        public StudentWrapper<Student> Put(Student student)
        {
            Student updatedStudent = Repository.Update(student);
            LinkWrapper linkWrapper = new LinkWrapper(new Link(API_PATH, "PUT"), new List<Link> 
            { 
                new Link(API_PATH, "GET"),
                new Link($"{API_PATH}/{student.Id}", "GET"),
                new Link(API_PATH, "POST"),
                new Link($"{API_PATH}/{student.Id}", "DELETE")
            });
            return new StudentWrapper<Student>(updatedStudent, linkWrapper);
        }

        [HttpDelete]
        [Exception]
        public StudentWrapper<Student> Delete([FromUri] int id)
        {
            Student deletedStudent = Repository.Delete(id);
            LinkWrapper linkWrapper = new LinkWrapper(new Link($"{API_PATH}/{id}", "DELETE"), new List<Link> { new Link(API_PATH, "GET"), new Link(API_PATH, "POST") });
            return new StudentWrapper<Student>(deletedStudent, linkWrapper);
        }
    }
}
