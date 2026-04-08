using A_P_SmartHub.Graphics.MainGrap;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace A_P_SmartHub.Graphics.Additional
{
    internal class smtpClientMail
    {
        public void SendMail(VerificationCodeWindow verificationCode, Register register)
        {


            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", "mqsorowexagrnmng"),
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

