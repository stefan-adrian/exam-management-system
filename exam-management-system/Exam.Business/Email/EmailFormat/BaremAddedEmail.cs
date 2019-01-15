namespace Exam.Business.Email.EmailFormat
{
    public class BaremAddedEmail : IGenericEmail
    {
        public string Message { get; private set; }

        public BaremAddedEmail(string to, Domain.Entities.Exam exam)
        {
            this.Message = "To: " + to + "\r\n" +
                           "Subject: Barem added for " + exam.Course.Name + "\r\n" +
                           "Content-Type: text/plain; charset=us-ascii\r\n\r\n" +
                           "The barem for " + exam.Course.Name +
                           " exam was added.";
        }

        public string GetEmail()
        {
            return this.Message;
        }
    }
}
