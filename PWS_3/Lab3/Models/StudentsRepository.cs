using Lab3.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class StudentsRepository
    {
        private StudentContext Context { get; set; } = new StudentContext();


        public List<Student> Get()
        {
            return Context.Students.ToList();
        }

        public Student Get(int id)
        {
            if (!StudentExists(id))
            {
                throw new StudentException(81, "There is no student with such Id of " + id);
            }
            return Context.Students.First(student => student.Id == id);
        }

        public Student Add(Student student)
        {
            if (student == null || string.IsNullOrEmpty(student.Name))
            {
                throw new StudentException(82, "Invalid student instance. Name is required");
            }
            Context.Students.Add(student);
            Context.SaveChanges();
            return student;
        }

        public Student Update(Student student)
        {
            if (student == null || !StudentExists(student.Id))
            {
                throw new StudentException(83, "There is no student with such Id of " + student.Id);
            }
            Student targetStudent = Context.Students.First(st => st.Id == student.Id);
            if (!string.IsNullOrEmpty(student.Name))
            {
                targetStudent.Name = student.Name;
            }
            if (!string.IsNullOrEmpty(student.Phone))
            {
                targetStudent.Phone = student.Phone;
            }
            Context.SaveChanges();
            return targetStudent;
        }

        public Student Delete(int id)
        {
            if (!StudentExists(id))
            {
                throw new StudentException(84, "There is no student with such Id of " + id);
            }
            Student targetStudent = Context.Students.First(student => student.Id == id);
            Context.Students.Remove(targetStudent);
            Context.SaveChanges();
            return targetStudent;
        }


        private bool StudentExists(int? id)
        {
            return Context.Students.Any(student => student.Id == id);
        }
    }
}
