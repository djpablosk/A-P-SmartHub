using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Additional.ForgotPassword;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
using A_P_SmartHub.Weather;
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
        SQLITE_Users users = new SQLITE_Users();
        MySql mySql = new MySql();
        public Login()
        {
            InitializeComponent();

        }




        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

            

           // bool success = CheckLogin(users, mySql);
            //Teraz už 'await' nebude podčiarknuté
           // await System.Threading.Tasks.Task.Delay(5000);

            //if (!success)
            //{
            //    MessageBox.Show("Mail Or Password Is Incorrect");
            //    return;
            //}

            VisualStateManager.GoToElementState(this.MainRoot, "LoggingInState", true);

            await Task.Delay(5000);

            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow == null)
                return;

            mainWindow.SlideViewTransition(new MainDashboard(), true);

           // MessageBox.Show("ide to");

            await mySql.DataBase();

           
                //if (success)
                //{
                //    mainWindow.SlideViewTransition(new MainDashboard(), true);
                //    MessageBox.Show("ide to");
                //    mySql.DataBase();
                //}
            }
        



        //public bool CheckLogin(SQLITE_Users users, MySql mySql)
        //{

        //    bool checkHash = false;
        //    if (string.IsNullOrWhiteSpace(LoginMail.Text) ||
        //      string.IsNullOrWhiteSpace(LoginPasword.Password)) return false;
        //    users.LoggingInDB(LoginMail.Text);
        //    if (string.IsNullOrEmpty(users.FetchedMail)) return false;
        //    if (string.IsNullOrEmpty(users.FetchedHash)) return false;
        //    string tempMail = LoginMail.Text;

        //    if (users.FetchedMail == LoginMail.Text)
        //    {
        //        checkHash = BCrypt.Net.BCrypt.EnhancedVerify(LoginPasword.Password, users.FetchedHash);
        //    }



        //    if (users.FetchedMail == LoginMail.Text && checkHash == true)
        //    {
        //        SessionInfo.ID = users.GetUserId(tempMail);
        //        mySql.ReturnBasicFromDB(SessionInfo.ID);

        //        return true;
        //    }
        //    else if (users.FetchedMail != LoginMail.Text || checkHash != true)
        //    {

        //        if (users.FetchedMail == LoginMail.Text && checkHash)
        //        {
        //            SessionInfo.ID = users.GetUserId(tempMail);
        //            mySql.ReturnBasicFromDB(SessionInfo.ID);
        //            MessageBox.Show("login ok");
        //            return true;
        //        }
        //        else
        //        {
        //            MessageBox.Show(" Mail or Password is incorrect");
        //            return false;
        //        }
               
        //    }
        //    return false;
        //}
          
                
            
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

                mainWindow.SlideViewTransition(new newpasswordScreen(), true);
            }
        }
    }

}


