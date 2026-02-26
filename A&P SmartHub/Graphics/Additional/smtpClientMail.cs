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
                mail.From = new MailAddress("A&PSmarthub@gmail.com");
                mail.To.Add(register.Mail);
                mail.Subject = ("2FA Code");
                mail.IsBodyHtml = true;
                mail.Body = @$" / <!DOCTYPE html>
    <html>
    <head>
      <meta charset=""UTF-8"">
      <title>A&P SmartHub - Verification Code</title>
    </head>
    <body style=""margin:0; padding:0; background-color:#0d0d0f; font-family:Arial, sans-serif;"">

      <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td align=""center"" style=""padding:50px 20px;"">

            <!-- Main Container -->
            <table width=""520"" cellpadding=""0"" cellspacing=""0"" 
                   style=""background:#111217; border-radius:12px; padding:40px; box-shadow:0 0 40px rgba(0,0,0,0.6);"">

              <!-- Logo -->
              <tr>
                <td align=""center"" style=""padding-bottom:30px;"">
                  <img src=""https://i.ibb.co/jZv39Cgj/ap-Logo.png"" 
                       alt=""A&P SmartHub"" 
                       width=""160"" 
                       style=""display:block;"">
                </td>
              </tr>

              <!-- Heading -->
              <tr>
                <td align=""center"" style=""font-size:22px; font-weight:bold; color:#ffffff; padding-bottom:10px;"">
                  Verify your login
                </td>
              </tr>

              <!-- Subtext -->
              <tr>
                <td align=""center"" style=""font-size:14px; color:#b3b3b3; padding-bottom:30px;"">
                  Use this code to continue. Expires in 5 minutes.
                </td>
              </tr>

              <!-- Code Box -->
              <tr>
                <td align=""center"">
                  <div style=""
                    display:inline-block;
                    padding:18px 40px;
                    font-size:30px;
                    font-weight:bold;
                    letter-spacing:6px;
                    color:#ffffff;
                    border-radius:10px;
                    background:linear-gradient(90deg, #4cc9f0, #7209b7, #f4a261);
                  "">
                    {verificationCode.RandomCode}
                  </div>
                </td>
              </tr>

              <!-- Divider -->
              <tr>
                <td align=""center"" style=""padding:35px 0 20px 0;"">
                  <div style=""height:1px; width:100%; background:linear-gradient(90deg,#4cc9f0,#7209b7,#f4a261); opacity:0.3;""></div>
                </td>
              </tr>

              <!-- Footer -->
              <tr>
                <td align=""center"" style=""font-size:12px; color:#777;"">
                  If you didn’t request this, ignore this email.
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
