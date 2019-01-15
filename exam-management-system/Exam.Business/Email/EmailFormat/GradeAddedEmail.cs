namespace Exam.Business.Email
{
    public class GradeAddedEmail : IGenericEmail
    {
        public string Message { get; private set; }

        public GradeAddedEmail(Domain.Entities.Grade grade)
        {
            this.Message = "To: " + grade.Student.Email + "\r\n" +
                           "Subject: Grade added for " + grade.Exam.Course.Name + "\r\n" +
                           "Content-Type: text/plain; charset=us-ascii\r\n\r\n" +
                           "Your grade at " + grade.Exam.Course.Name +
                           " exam was added.";
        }

        public string GetEmail()
        {
            return this.Message;
        }
    }
}
