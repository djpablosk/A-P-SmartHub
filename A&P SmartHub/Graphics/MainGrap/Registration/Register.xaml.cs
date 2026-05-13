using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Interfaces;
using BCrypt.Net;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VerificationCodeWindow = A_P_SmartHub.Graphics.Additional.VerificationCodeWindow;
using System.Net.NetworkInformation;
using System.Net;


namespace A_P_SmartHub.Graphics.MainGrap
{
        public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        public string Mail { get; set; }
        private string Password { get; set; }
        private string PassHash { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            smtpClientMail smtpClientMail = new smtpClientMail();
            VerificationCodeWindow verificationCode = new VerificationCodeWindow();
            
            MySQL_Users users = new MySQL_Users();
          

            
            if (Passw1.Password  != Passw2.Password)
            {
                MessageBox.Show("Password do not match");
                return;
            }
            else if (Passw1.Password.Length < 8)
            {
                MessageBox.Show("This password is too weak, please use password with 8 or more chars");
                return;
            }
            else if (!EmailRegWind.Text.Contains("@"))
            {
                MessageBox.Show("Invalid Mail format, maybe you're missing '@'");
                return;
            }
            else
            {
                Password = Passw1.Password;
                Mail = EmailRegWind.Text;
                PassHash = BCrypt.Net.BCrypt.EnhancedHashPassword(Password);
                SessionInfo.Mail = Mail;

                bool result = users.IsMailInDB(Mail);

                if (result)
                {
                    MessageBox.Show("Looks Like This Mail is already Used  Please Log In");
                    if (mainWindow != null)
                    {
                        mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
                    }
                }
                else
                {
                    SessionInfo.Mail = Mail;
                    verificationCode.Mail = EmailRegWind.Text;
                    verificationCode.PassHash = this.PassHash;
                    mainWindow.MainDisplay.Content = verificationCode;
                    smtpClientMail.SendMail(verificationCode, this);
                    mainWindow.MainDisplay.Content = verificationCode;
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            PasswSEE.Text = Passw1.Password;
            SeePassword.Visibility = Visibility.Visible;
            notSeePassword.Visibility = Visibility.Collapsed;

            PasswSEE2.Text = Passw2.Password;
            SeePassword2.Visibility = Visibility.Visible;
            notSeePassword2.Visibility = Visibility.Collapsed;

        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            SeePassword2.Visibility = Visibility.Collapsed;
            notSeePassword2.Visibility = Visibility.Visible;
            SeePassword.Visibility = Visibility.Collapsed;
            notSeePassword.Visibility = Visibility.Visible;
        }
    }
}