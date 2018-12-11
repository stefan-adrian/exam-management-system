using System;
using System.Collections.Generic;
using System.Reflection;

namespace Exam.Domain.Entities
{
    public class Course : Entity
    {
        private Course()
        {
            // EF
        }

        public string Name { get; private set; }

        public int Year { get; private set; }

        public Professor Professor { get; private set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

        public List<Exam> Exams { get; private set; }

        public Course(string name, int year, Professor professor) : base(Guid.NewGuid())
        {
            Name = name;
            Year = year;
            Professor = professor;
            StudentCourses=new List<StudentCourse>();
        }

        public Course(string name, int year) : base(Guid.NewGuid())
        {
            Name = name;
            Year = year;
        }

        public void SetPropertyValue(string propertyName, object val)
        {
            Type objType = this.GetType();
            PropertyInfo propertyInfo = GetFieldInfo(objType, propertyName);
            if(propertyInfo.CanWrite)
                propertyInfo.SetValue(this, val);
        }

        private PropertyInfo GetFieldInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo;
            // for searching fields in upper classes (in case of inheritance)
            do
            {
                propertyInfo = type.GetProperty(propertyName);
                type = type.BaseType;
            } while (propertyInfo == null && type != null);
            return propertyInfo;
        }
    }
}
