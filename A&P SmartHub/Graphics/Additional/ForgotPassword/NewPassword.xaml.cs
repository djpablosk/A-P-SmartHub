using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Login;
using System;
using System.Windows;
using System.Windows.Controls;

namespace A_P_SmartHub.Graphics.Additional.ForgotPassword
{
    public partial class NewPassword : UserControl
    {
        public string ResMail { get; set; }
        MySQL_Users sQLITE_Users = new MySQL_Users();
        public NewPassword()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (ResPasB0.Text != ResPasB1.Text)
            {
                MessageBox.Show("Password do not match");
                return;
            }
            else if (ResPasB1.Text.Length < 8)
            {
                MessageBox.Show("This password is too weak, please use password with 8 or more chars");
                return;
            }
            else
            {
                string pass = BCrypt.Net.BCrypt.EnhancedHashPassword(ResPasB1.Text);
                sQLITE_Users.UpdateHashInDb(ResMail, pass);
                
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
                }
            }
        }
    }
}
