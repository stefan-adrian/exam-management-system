namespace Exam.Business.Email.EmailFormat
{
    public class StudentDoesNotAgreeEmail : IGenericEmail
    {
        public string Message { get; private set; }

        public StudentDoesNotAgreeEmail(Domain.Entities.Grade grade)
        {
            this.Message = "To: " + grade.Exam.Course.Professor.Email + "\r\n" +
                           "Subject: Student does not agree with grade at " + grade.Exam.Course.Name + "\r\n" +
                           "Content-Type: text/plain; charset=us-ascii\r\n\r\n" +
                           "Student " + grade.Student.FirstName + " " + grade.Student.LastName +
                           " does not agree with his grade at " + grade.Exam.Course.Name + " exam.";
        }

        public string GetEmail()
        {
            return this.Message;
        }
    }
}
