using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Additional.ForgotPassword;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
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


namespace A_P_SmartHub.Graphics.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        public Login()
        {
            InitializeComponent();

        }




        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToElementState(this.MainRoot, "LoggingInState", true);


            //Teraz už 'await' nebude podčiarknuté
            await Task.Delay(5000);

            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                SQLITE_Users users = new SQLITE_Users();
                MySql mySql = new MySql();
                bool success = CheckLogin(users, mySql); // make CheckLogin return bool
                if (success)
                {

                    mainWindow.SlideViewTransition(new MainDashboard(), true);
                    MessageBox.Show("ide to");
                    mySql.DataBase();



               // }


            }
        }

        public bool CheckLogin(SQLITE_Users users, MySql mySql)
        {
            string tempMail = LoginMail.Text;
            users.LoggingInDB(LoginMail.Text);
            bool checkHash = false;
            if (users.FetchedMail == LoginMail.Text)
            {
                checkHash = BCrypt.Net.BCrypt.EnhancedVerify(LoginPasword.Password, users.FetchedHash);
            }

            if (users.FetchedMail == LoginMail.Text && checkHash == true)
            {
                MessageBox.Show("login ok");
                return true;
                if (users.FetchedMail == LoginMail.Text && checkHash == true)
                {
                    SessionInfo.ID = users.GetUserId(tempMail);
                    mySql.ReturnBasicFromDB(SessionInfo.ID);
                    MessageBox.Show("login ok");
                    return true;


                }
                else if (users.FetchedMail != LoginMail.Text || checkHash != true)
                {
                    MessageBox.Show(" Mail or Password is incorrect");
                }
                return false;

            }
            return false;
        }


        



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;


            if (mainWindow != null)
            {

                mainWindow.SlideViewTransition(new Register(), true);
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;


            if (mainWindow != null)
            {

                mainWindow.SlideViewTransition(new NewPasswordScreen(), true);
            }
        }
    }

}


