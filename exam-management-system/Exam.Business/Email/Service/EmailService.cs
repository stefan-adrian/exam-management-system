using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Exam.Business.Email
{
    public class EmailService : IEmailService
    {
        private static readonly string[] Scopes = { GmailService.Scope.GmailSend };
        private static readonly string ApplicationName = "Exam Management System";
        private readonly UserCredential credential;
        private readonly GmailService gmailService;

        public EmailService()
        {
            try
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    this.credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                // Create Gmail API Service
                gmailService = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SendEmail(IGenericEmail email)
        {
            var message = new Google.Apis.Gmail.v1.Data.Message();
            message.Raw = ConvertBase64UrlEncode(email.GetEmail());
            gmailService.Users.Messages.Send(message, "me").Execute();
        }

        private string ConvertBase64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
