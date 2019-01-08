using Exam.Domain.Entities;
using Exam.Persistance;
using Exam.Test.TestUtils;

namespace Exam.Test.Integration
{
    public static class DatabasePopulationData
    {
        public static async System.Threading.Tasks.Task PopulateTestDatabaseAsync(ExamContext examContext)
        {
            Student student = StudentTestUtils.GetStudent();
            Course course = CourseTestUtils.GetCourse();
            await examContext.AddNewAsync(student);
            await examContext.AddNewAsync(StudentTestUtils.GetStudent2());
            await examContext.AddNewAsync(course);
            await examContext.AddNewAsync(CourseTestUtils.GetCourse2());
            await examContext.AddNewAsync(ProfessorTestUtils.GetProfessor());
            await examContext.AddNewAsync(ProfessorTestUtils.GetProfessor2());
            await examContext.AddNewAsync(StudentCourseTestUtils.GetStudentCourse(student.Id, course.Id));
            await examContext.AddNewAsync(ExamTestUtils.GetExam());
            await examContext.AddNewAsync(ClassroomTestUtils.GetClassroom());
            await examContext.AddNewAsync(GradeTestUtils.GetInitialStateGrade());
            await examContext.AddNewAsync(ClassroomAllocationTestUtils.GetClassroomAllocation());
            await examContext.AddNewAsync(GradeTestUtils.GetGradeWithValue());
            await examContext.SaveAsync();
        }
    }
}
