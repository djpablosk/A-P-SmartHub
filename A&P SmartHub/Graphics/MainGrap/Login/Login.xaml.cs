using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Additional.ForgotPassword;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
using A_P_SmartHub.Weather;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Media;
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
       
        MySql mySql = new MySql();
        MySQL_Users users = new MySQL_Users();
        public Login()
        {
           
            InitializeComponent();

        }

       


        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

         

            bool success = CheckLogin(users, mySql);
           
          

            if (!success)
            {
                MessageBox.Show("Mail Or Password Is Incorrect");
                return;
            }


            await Task.Delay(2300);

            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow == null)
                return;

           

            if (success)
            {
                HomePage homePage = new HomePage();
    
                await mySql.DataBase();

                mainWindow.SlideViewTransition(new MainDashboard(), true);
              //  MessageBox.Show("ide to");
              
            }
        }

        public bool CheckLogin( MySQL_Users users , MySql mySql) 
        {

            bool checkHash = false;
            
            if (string.IsNullOrWhiteSpace(LoginMail.Text) || string.IsNullOrWhiteSpace(LoginPasword.Password)) 
                return false;
                    
            users.LoggingInDB(LoginMail.Text);
            if (string.IsNullOrEmpty(users.FetchedMail)) return false;
            if (string.IsNullOrEmpty(users.FetchedHash)) return false;
            string tempMail = LoginMail.Text;

            if (users.FetchedMail == LoginMail.Text)
            {
                checkHash = BCrypt.Net.BCrypt.EnhancedVerify(LoginPasword.Password, users.FetchedHash);
            }




            if (users.FetchedMail == LoginMail.Text && checkHash)
            {
                SessionInfo.ID = users.GetUserId(tempMail);
               

                mySql.LoadDevices(SessionInfo.ID);
                
                return true;
            }

            else
            {
             
                return false;
            }
          

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


