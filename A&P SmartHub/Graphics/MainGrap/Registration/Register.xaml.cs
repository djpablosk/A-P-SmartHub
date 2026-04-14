using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Interfaces;
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
            VerificationCodeWindow verificationCode = new VerificationCodeWindow();
            SQLITE_Users sQLITE_Users = new SQLITE_Users();



            //if (Passw1.Text != Passw2.Text)
            //{
            //    MessageBox.Show("Password do not match");
            //    return;
            //}
            //else if (Passw1.Text.Length < 8)
            //{
            //    MessageBox.Show("This password is too weak, please use password with 8 or more chars");
            //    return;
            //}
            //else if (!EmailRegWind.Text.Contains("@"))
            //{
            //    MessageBox.Show("Invalid Mail format, maybe you're missing '@'");
            //    return;
            //}
            //else
            //{
            //    Password = Passw1.Text;
            //    Mail = EmailRegWind.Text;
               var mainWindow = Window.GetWindow(this) as MainWindow;

            //    verificationCode.Mail = EmailRegWind.Text;
            //    verificationCode.PassHash = BCrypt.Net.BCrypt.EnhancedHashPassword(Password);
               mainWindow.MainDisplay.Content = verificationCode;
            //    smtpClientMail.SendMail(verificationCode, this);
            //}


            // 2. Ak sme ho našli, povieme mu, nech spustí SVOJU funkciu na prechod
           
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
            var mainWindow = Window.GetWindow(this) as MainWindow;

            // 2. Ak sme ho našli, povieme mu, nech spustí SVOJU funkciu na prechod
            if (mainWindow != null)
            {
                // If the user control class is named "Login" inside the namespace A_P_SmartHub.Graphics.Login,
                // fully qualify the type to avoid the namespace-vs-type ambiguity.
                // If the actual class name is different, replace the final "Login" with the real class name.
                mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
        }
    }
}