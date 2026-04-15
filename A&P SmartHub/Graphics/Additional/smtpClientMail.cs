using A_P_SmartHub.Graphics.MainGrap;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using DotNetEnv;

 

namespace A_P_SmartHub.Graphics.Additional
{
    internal class smtpClientMail
    {

        public void SendCode(VerificationCodeWindow verification)
        {
            Env.Load();
            string MailCode = Environment.GetEnvironmentVariable("mailPass");
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", MailCode),
                EnableSsl = true,
            };
            var mail = new MailMessage();
            mail.From = new MailAddress("A&PSmarthub@gmail.com");
            mail.To.Add("alexozaniakk@gmail.com");//pridam aby to dalo tomu co zabudol heslo
            mail.Subject = ("Your Code For Resseting Password is here!");
            mail.IsBodyHtml = true;
            mail.Body = @$"
```html
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <title>Password Reset</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">

  <div style=""max-width: 500px; margin: auto; background: white; padding: 20px; border-radius: 10px; text-align: center;"">
    
    <h2>Password Reset</h2>
    
    <p>Your code for resetting your password is:</p>
    
    <div style=""font-size: 24px; font-weight: bold; background: #f0f0f0; padding: 15px; border-radius: 8px; letter-spacing: 3px;"">
      {verification.RandomCode}
    </div>
    
    <p style=""margin-top: 20px; font-size: 12px; color: gray;"">
      If you didn’t request this, just ignore this email.
    </p>

  </div>

</body>
</html>
```
";
            smtp.Send(mail);
                }
        public void SendMail(VerificationCodeWindow verificationCode, Register register)
        {
            Env.Load();
             string MailCode = Environment.GetEnvironmentVariable("mailPass");


        var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", MailCode),// zmenit

                EnableSsl = true
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("A&PSmarthub@gmail.com");
            mail.To.Add(register.Mail);
            mail.Subject = ("2FA Code");
            mail.IsBodyHtml = true;
            mail.Body = @$" <!DOCTYPE html>
<html>
<head>
<meta charset=""UTF-8"">
<title>A&P SmartHub</title>
</head>
<body style=""margin:0; padding:0; background-color:#000;"">

<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:#000;"">
  <tr>
    <td align=""center"">

      <table width=""400"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:#111; margin:50px 0; border-radius:10px;"">
        <tr>
          <td align=""center"" style=""padding:40px; font-family:Arial, sans-serif;"">

            <h2 style=""color:#ffffff; margin:0 0 20px 0;"">A&P SmartHub</h2>

            <p style=""color:#aaaaaa; margin:0 0 15px 0;"">Tvoj overovací kód:</p>

            <div style=""background-color:#222; padding:20px; border-radius:6px; font-size:26px; color:#ffffff; font-weight:bold; letter-spacing:6px;"">
              {verificationCode.RandomCode}
            </div>

          </td>
        </tr>
      </table>

    </td>
  </tr>
</table>

</body>
</html>";

            smtp.Send(mail);

        }
    }
}

