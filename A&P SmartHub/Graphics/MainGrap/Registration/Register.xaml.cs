using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
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



        private string Password { get; set; }
        public string Mail { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            smtpClientMail smtpClientMail = new smtpClientMail();
            VerificationCodeWindow verificationCode = new VerificationCodeWindow();
            SQLITE_Users sQLITE_Users = new SQLITE_Users();




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
                Password = Passw1.Text;
                Mail = EmailRegWind.Text;
                
                verificationCode.Mail = EmailRegWind.Text;
                verificationCode.PassHash = BCrypt.Net.BCrypt.EnhancedHashPassword(Password);
                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow.MainDisplay.Content = verificationCode;
                smtpClientMail.SendMail(verificationCode, this);

            }

        }







        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
