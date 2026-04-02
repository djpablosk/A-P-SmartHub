using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.MainGrap.Registration;
using BCrypt.Net;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

namespace A_P_SmartHub.Graphics.MainGrap
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    /// 

        
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }


        public string Mail { get; set; }
        private string Password { get; set; }
        private string PassHash { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            smtpClientMail smtpClientMail = new smtpClientMail();
            VerificationCodeWindow verificationCode  = new VerificationCodeWindow();




            if (Passw1.Text != Passw2.Text)
            {
                MessageBox.Show("Password do not match");
                return;
            }
            else if (Passw1.Text.Length < 8)
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

                Mail = EmailRegWind.Text;
                Password = Passw1.Text;
                PassHash = BCrypt.Net.BCrypt.EnhancedHashPassword(Password);
                bool uncrypPass = BCrypt.Net.BCrypt.EnhancedVerify(Password, PassHash);
                var mainWindow = Window.GetWindow(this) as MainWindow;
                smtpClientMail.SendMail(verificationCode, this);
                mainWindow.MainDisplay.Content = verificationCode;
            }
            

        }
     
        


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           //
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.Close();
        }
    }
}
