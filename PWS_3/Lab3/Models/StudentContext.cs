using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        
        public StudentContext() : base("SQLDefault")
        {
            Students.Load();
        }
    }
}
