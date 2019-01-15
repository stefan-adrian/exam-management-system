namespace Exam.Business.Email
{
    public interface IEmailService
    {
        void SendEmail(IGenericEmail email);
    }
}
