using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Login;
using System;
using System.Collections.Generic;
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

namespace A_P_SmartHub.Graphics.Additional.ForgotPassword
{
    /// <summary>
    /// Interaction logic for NewPassword.xaml
    /// </summary>
    public partial class NewPassword : UserControl
    {
        public string ResMail { get; set; }
        public NewPassword()
        {
            
            InitializeComponent();
        }

       
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
           SQLITE_Users sQLITE_Users = new SQLITE_Users();

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
