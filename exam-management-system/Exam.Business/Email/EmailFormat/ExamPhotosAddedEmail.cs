namespace Exam.Business.Email.EmailFormat
{
    public class ExamPhotosAddedEmail : IGenericEmail
    {
        public string Message { get; private set; }

        public ExamPhotosAddedEmail(Domain.Entities.Grade grade)
        {
            this.Message = "To: " + grade.Student.Email + "\r\n" +
                           "Subject: Exam images added to " + grade.Exam.Course.Name + "\r\n" +
                           "Content-Type: text/plain; charset=us-ascii\r\n\r\n" +
                           "The teacher has uploaded imaged with your " +
                           grade.Exam.Course.Name + " exam.";
        }

        public string GetEmail()
        {
            return this.Message;
        }
    }
}
