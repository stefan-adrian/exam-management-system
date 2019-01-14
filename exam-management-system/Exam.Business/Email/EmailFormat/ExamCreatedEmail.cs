namespace Exam.Business.Email
{
    public class ExamCreatedEmail : IGenericEmail
    {
        public string Message { get; private set; }

        public ExamCreatedEmail(string to, Domain.Entities.Exam exam)
        {
            this.Message = "To: " + to + "\r\n" +
                      "Subject: Exam Created for " + exam.Course.Name + "\r\n" +
                      "Content-Type: text/plain; charset=us-ascii\r\n\r\n" +
                      "The exam for " + exam.Course.Name + " will take place on " + exam.Date.ToString("dd MMMM yyyy") +
                      " at " +
                      exam.Date.ToString("hh:mm tt") +
                      "\nClassrooms allocated for the exam: " +
                      this.GetClassrooms(exam);
        }

        private string GetClassrooms(Domain.Entities.Exam exam)
        {
            string result = "";
            foreach (var classroomAllocation in exam.ClassroomAllocation)
            {
                result += classroomAllocation.Classroom.Location.ToUpper() + ", ";
            }

            result = result.Remove(result.Length - 2) + '.';

            return result;
        }

        public string GetEmail()
        {
            return this.Message;
        }
    }
}
