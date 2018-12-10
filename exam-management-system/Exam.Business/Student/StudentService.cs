using Exam.Domain.Entities;
using Exam.Domain.Interfaces;

namespace Exam.Business.Student
{
    public class StudentService : IStudentService
    {
        private readonly IReadRepository readRepository;

        private readonly IWriteRepository writeRepository;

        public StudentService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
        }

        public Domain.Entities.Student Create()
        {
            Professor professor=new Professor("6666666666","email","psd","name","name");
            Course course=new Course("Curs3333333",3,professor);
            Domain.Entities.Student student=new Domain.Entities.Student("0101010101","abc","abc","firstName","lastName",1);
            StudentCourse studentCourse=new StudentCourse();
            studentCourse.Student = student;
            studentCourse.Course = course;
            studentCourse.StudentId = student.Id;
            studentCourse.CourseId = course.Id;
            student.StudentCourses.Add(studentCourse);
            course.StudentCourses.Add(studentCourse);

            writeRepository.AddNewAsync(professor);
            writeRepository.AddNewAsync(student);
            writeRepository.AddNewAsync(course);

            writeRepository.SaveAsync();
            return student;
        }
    }
}
