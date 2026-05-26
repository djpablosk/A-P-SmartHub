using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        NewPassword newPassword1 = new NewPassword();
        public CodeScreen()
        {
            Random random = new Random();
            RandomCode = random.Next(100000, 1000000);

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

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            
            string test = ForgotPassCode.Text;

            

            if (int.TryParse(test, out int inputCode))
            {
                if (inputCode == RandomCode)
                {
              
                    //  MessageBox.Show("reset hesla ide ");

                    DependencyObject parent = this;
                

                    while (parent != null)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        if (parent is NewPasswordScreen newPasswordScreen)
                        {
                            newPassword1.ResMail = Mail;

                       

                            newPasswordScreen.ShowNewPasswordScreen(newPassword1);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Code broski;");
                }


            }
            else MessageBox.Show("This is not a number");

        }
          

        private void ForgotPassCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            //
        }
    }
}
