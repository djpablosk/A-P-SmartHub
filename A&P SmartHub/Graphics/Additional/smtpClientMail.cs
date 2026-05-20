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
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">

  <div style=""max-width: 500px; margin: auto; background: white; padding: 20px; border-radius: 10px; text-align: center;"">
    
    <h2>Password Reset</h2>
    
    <p>Your code for resetting your password is:</p>
    
    <div style=""font-size: 24px; font-weight: bold; background: #f0f0f0; padding: 15px; border-radius: 8px; letter-spacing: 3px;"">
      {screen.RandomCode}
    </div>
    
    <p style=""margin-top: 20px; font-size: 12px; color: gray;"">
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
                Gas sensor network has detected abnormal air quality levels.
              </p>

              <div style=""background:#2a1b1b; border-left:4px solid #ff4d4d; padding:14px; border-radius:8px; margin-bottom:18px;"">
                <p style=""margin:0; color:#ff6b6b; font-weight:bold; font-size:14px;"">
                  ⚠ Location: {HomeName}
                </p>
                <p style=""margin:6px 0 0; color:#ffb3b3; font-size:13px;"">
                  Measured gas value: {gasvalue}
                </p>
              </div>

              <table width=""100%"" cellpadding=""6"" cellspacing=""0"" border=""0""
                style=""margin-bottom:18px; font-size:12px; color:#cfcfcf;"">

                <tr style=""color:#888;"">
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
                Recommended action: ventilate the area immediately and avoid ignition sources.
              </p>

              <p style=""margin:10px 0 0; color:#6b7280; font-size:11px;"">
                SmartHub will continue real-time monitoring.
              </p>

            </td>
          </tr>

          <tr>
            <td style=""padding:14px 20px; background:#12141a; font-size:11px; color:#666;"">
              SmartHub IoT Core
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

