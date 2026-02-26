using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Graphics.MainGrap.Registration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace A_P_SmartHub.Graphics.Additional
{
    internal class smtpClientMail
    {
        public void SendMail(VerificationCodeWindow verificationCode,Register register)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", "mqsorowexagrnmng"),
                EnableSsl = true
            };
            
            var mail = new MailMessage();
            mail.From = new MailAddress("apsmarthub@gmail.com");
            mail.To.Add(register.Mail);
            mail.Subject = ("2FA Code");
            mail.IsBodyHtml = true;
            mail.Body = @$" {verificationCode.RandomCode}";

            smtp.Send(mail);

        }
    }
}
