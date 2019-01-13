using System;
using Exam.Business.Email.EmailFormat;

namespace Exam.Business.Email
{
    public interface IEmailService
    {
        void SendEmail(IEmailFormat email);
    }
}
