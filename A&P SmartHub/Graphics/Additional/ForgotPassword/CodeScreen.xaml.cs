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
    /// Interaction logic for CodeScreen.xaml
    /// </summary>
    public partial class CodeScreen : UserControl
    {
        public int RandomCode { get; set; }
        public string Mail { get; set; }
        public CodeScreen()
        {
            InitializeComponent();
            Random random = new Random();
            RandomCode= random.Next(100000, 1000000);

        }
        
        
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string test = ForgotPassCode.Text;
            int inputCode = int.Parse(test);
            if (inputCode == RandomCode)
            {
                MessageBox.Show("reset hesla ide ");
                NewPassword newPassword = new NewPassword();
                DependencyObject parent = this;
              
                while (parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    if (parent is NewPasswordScreen newPasswordScreen)
                    {
                        newPassword.ResMail = Mail;
                        newPasswordScreen.ShowNewPasswordScreen(newPassword);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong Code broski;");
            }

            
            }
        

        
    }
}
