namespace Exam.Business.Classroom.Exception
{
    public class ClassroomLocationAlreadyExistsException : System.Exception
    {
        public ClassroomLocationAlreadyExistsException(string Location) : base("Classroom with location " + Location + " already exists!")
        {
            
        }
    }
}
