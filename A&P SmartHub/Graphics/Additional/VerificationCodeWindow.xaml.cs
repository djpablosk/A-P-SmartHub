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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for VerificationCodeWindow.xaml
    /// </summary>
    public partial class VerificationCodeWindow : UserControl
    {
        public int RandomCode {  get; set; }

        public VerificationCodeWindow()
        {
            InitializeComponent();
            Random random = new Random();
          RandomCode = random.Next(100000,1000000);
            
        }
       
       
        
        
      
            
       
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifCodeInput.Text == RandomCode.ToString())
            {
                MessageBox.Show("verification succesful");
            }
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                //smtpClientMail smtpClientMail = new smtpClientMail();
                // smtpClientMail.SendMail(register);

                if (mainWindow != null)
                {

                    // mainWindow.MainDisplay.Content = new ();    este doriesim

                }
            }
        }
    }
}
