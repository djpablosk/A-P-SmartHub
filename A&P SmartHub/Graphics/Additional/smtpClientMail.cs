using A_P_SmartHub.Graphics.MainGrap;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using DotNetEnv;
using A_P_SmartHub.Graphics.Additional.ForgotPassword;



namespace A_P_SmartHub.Graphics.Additional
{
    internal class smtpClientMail
    {
        public string ResetMail {  get; set; }
        public async Task SendCode( CodeScreen screen, MailScreen mailScreen)
        {
           
            ResetMail = mailScreen.ForgotMail;
            screen.Mail = ResetMail;
            
            Env.Load();
            string MailCode = Environment.GetEnvironmentVariable("mailPass");
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", MailCode),
                EnableSsl = true,
            };
            var mail = new MailMessage();
            mail.From = new MailAddress("A&PSmarthub@gmail.com");
            mail.To.Add(ResetMail);
            mail.Subject = ("Your Code For Resseting Password is here!");
            mail.IsBodyHtml = true;
            mail.Body = @$"

<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <title>Password Reset</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #0e0f12; padding: 20px; color: #ffffff; margin: 0;"">

  <div style=""max-width: 500px; margin: auto; background: #16181d; padding: 30px; border-radius: 12px; text-align: center; border: 1px solid #2d313a;"">
    
    <h2 style=""color: #ffffff; margin-top: 0;"">Password Reset</h2>
    
    <p style=""color: #a0aec0; font-size: 15px; margin-bottom: 25px;"">Your code for resetting your password is:</p>
    
    <div style=""font-size: 28px; font-weight: bold; background: #20232a; color: #00A870; padding: 15px; border-radius: 8px; letter-spacing: 5px; border: 1px solid #333842;"">
      {screen.RandomCode}
    </div>
    
    <p style=""margin-top: 30px; font-size: 12px; color: #6b7280;"">
      If you didn’t request this, just ignore this email.
    </p>

  </div>

</body>
</html>
";
            smtp.Send(mail);
                }
       
        public void SendMail(VerificationCodeWindow verificationCode, Register register)
        {
            SessionInfo.Mail = register.Mail;
            Env.Load();
             string MailCode = Environment.GetEnvironmentVariable("mailPass");


        var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", MailCode),

                EnableSsl = true
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("A&PSmarthub@gmail.com");
            mail.To.Add(register.Mail);
            mail.Subject = ("2FA Code");
            mail.IsBodyHtml = true;
            mail.Body = @$"<!DOCTYPE html>
<html>
<head>
<meta charset=""UTF-8"">
<title>A&P SmartHub</title>
</head>
<body style=""margin:0; padding:0; background-color:#0e0f12;"">

<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:#0e0f12;"">
  <tr>
    <td align=""center"">

      <table width=""400"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:#16181d; margin:50px 0; border-radius:12px; border: 1px solid #2d313a;"">
        <tr>
          <td align=""center"" style=""padding:40px; font-family:Arial, sans-serif;"">

            <h2 style=""color:#ffffff; margin:0 0 20px 0; font-size: 24px;"">A&P SmartHub</h2>

            <p style=""color:#a0aec0; margin:0 0 25px 0; font-size: 15px;"">Your verification code:</p>

            <div style=""background-color:#20232a; border: 1px solid #333842; padding:20px; border-radius:8px; font-size:28px; color:#00A870; font-weight:bold; letter-spacing:6px;"">
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

            smtp.SendMailAsync(mail);

        }

        public async Task GasAlert(string adresa, string UserName, string HomeName,int gasvalue)
        {
            Env.Load();
            string MailCode = Environment.GetEnvironmentVariable("mailPass");

            var smt = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("apsmarthub@gmail.com", MailCode),
                EnableSsl = true
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("apsmarthub@gmail.com");
            mail.To.Add(adresa);
            mail.Subject = "Critical Safety Alert: Gas Level High";
            mail.IsBodyHtml = true;

            mail.Body = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <title>Gas Alert</title>
</head>

<body style=""margin:0; padding:0; background:#0e0f12; font-family:Arial, sans-serif;"">

  <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
    <tr>
      <td align=""center"" style=""padding:40px 10px;"">

        <table width=""420"" cellpadding=""0"" cellspacing=""0"" border=""0""
          style=""background:#16181d; border-radius:14px; overflow:hidden;"">

          <tr>
            <td style=""background:#1c1f26; padding:14px 20px; font-size:12px; color:#888;"">
              A&P SmartHub • Safety System
            </td>
          </tr>

          <tr>
            <td style=""padding:28px;"">

              <h2 style=""margin:0 0 10px; color:#ffffff; font-size:20px;"">
                Hello, {UserName}
              </h2>

              <p style=""margin:0 0 18px; color:#b5b5b5; font-size:14px; line-height:1.5;"">
                Gas sensor has detected abnormal air quality levels.
              </p>

              <div style=""background:#2a1b1b; border-left:4px solid #ff4d4d; padding:14px; border-radius:8px; margin-bottom:18px;"">
                <p style=""margin:0; color:#ff6b6b; font-weight:bold; font-size:14px;"">
                  ⚠ Measured gas value: {gasvalue}% - DANGEROUS
                </p>
              </div>

              <table width=""100%"" cellpadding=""6"" cellspacing=""0"" border=""0""
                style=""margin-bottom:18px; font-size:12px; color:#cfcfcf;"">

                <tr style=""color:#00FF44;"">
                  <td>SAFE</td>
                  <td>0 - 300</td>
                </tr>
                <tr style=""color:#f5c542;"">
                  <td>MODERATE</td>
                  <td>301 - 700</td>
                </tr>
                <tr style=""color:#ff4d4d;"">
                  <td>DANGEROUS</td>
                  <td>701+</td>
                </tr>

              </table>

              <p style=""margin:0; color:#9aa0a6; font-size:13px; line-height:1.6;"">
                Recommended action: ventilate the area immediately.
              </p>

              <p style=""margin:10px 0 0; color:#6b7280; font-size:11px;"">
                SmartHub will continue real-time monitoring.
              </p>

            </td>
          </tr>

          <tr>
            <td style=""padding:14px 20px; background:#12141a; font-size:11px; color:#666;"">
              A&P SmartHub • Slovakia
            </td>
          </tr>

        </table>

      </td>
    </tr>
  </table>

</body>
</html>
";

            await  smt.SendMailAsync(mail);
        }
    }
}

