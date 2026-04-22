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
       
        
        public MailScreen()
        {
            
            InitializeComponent();
        }
       public string ForgotMail {  get; set; }

        private void SendCode_Click(object sender, RoutedEventArgs e)
        {
            NewPassword password = new NewPassword();
           

            ForgotMail = forgotPassMail.Text;
            
            smtpClientMail smtpClientMail = new smtpClientMail();
            SQLITE_Users users = new SQLITE_Users();
            CodeScreen codeScreen   = new CodeScreen();
            
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
                    if (parent is NewPasswordScreen newPasswordScreen)
                    {
                        
                        newPasswordScreen.ShowCodeScreen(codeScreen); // ✅ SAME OBJECT
                        break;
                    }
                }
                smtpClientMail.SendCode(codeScreen, this);



            }
        }


       
    }
}
