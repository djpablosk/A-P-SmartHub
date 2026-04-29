using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Login = A_P_SmartHub.Graphics.Login.Login;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
using A_P_SmartHub.Graphics.Additional.ForgotPassword;


namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for VerificationCodeWindow.xaml
    /// </summary>
    public partial class VerificationCodeWindow : UserControl

    {
        public int RandomCode { get; set; }
        public string Mail { get; set; }
        public string PassHash { get; set; }
        public VerificationCodeWindow()
        {
            InitializeComponent();
            Random random = new Random();
            RandomCode = random.Next(100000, 1000000);
            


        }








        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (VerifCodeInput.Text == RandomCode.ToString())
            {
                SQLITE_Users sQLITE_Users = new SQLITE_Users();

                sQLITE_Users.CreateDB();


                MessageBox.Show("verification successful");
                if (mainWindow != null)
                {
                    mainWindow.MainDisplay.Content = new HomeSetup();
                }
                else
                {
                    MessageBox.Show("Verification Code Seems to not Match Our System");
                }


                
            }
        }
    }
}




