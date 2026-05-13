using A_P_SmartHub.Databazicky;
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
    /// Interaction logic for MailScreen.xaml
    /// </summary>
    public partial class MailScreen : UserControl
    {
        CodeScreen codeScreen = new CodeScreen();
        smtpClientMail smtpClientMail = new smtpClientMail();
       MySQL_Users users = new MySQL_Users();
        NewPassword password = new NewPassword();

        public MailScreen()
        {
            
            InitializeComponent();
        }
       public string ForgotMail {  get; set; }

        private void SendCode_Click(object sender, RoutedEventArgs e)
        {
           
           

            ForgotMail = forgotPassMail.Text;
            codeScreen.Mail = ForgotMail;
            
            
            
            
            string exists = users.GetUserId(forgotPassMail.Text);

            if (exists.IsWhiteSpace())
            {
                MessageBox.Show("This Mail Was Not Found In Our System Maybe You Want To Try Another Mail");
            }
            else
            {


                // Find the parent NewPasswordScreen in the visual tree and request it to show the CodeScreen
                DependencyObject parent = this;
                while (parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    if (parent is NewPasswordScreen  newPasswordScreen)
                    {
                        
                        newPasswordScreen.ShowCodeScreen(codeScreen); 
                        break;
                    }
                }
                smtpClientMail.SendCode(codeScreen, this);



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
    }
}
