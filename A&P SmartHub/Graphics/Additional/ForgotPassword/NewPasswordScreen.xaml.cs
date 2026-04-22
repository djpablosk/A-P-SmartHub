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
    /// Interaction logic for NewPasswordScreen.xaml
    /// </summary>
    public partial class NewPasswordScreen : UserControl
    {
        public NewPasswordScreen()
        {
            InitializeComponent();
            NewPasswordContentControl.Content = new MailScreen();
        }

        // Called by child screens to switch the displayed content to the code entry screen
        public void ShowCodeScreen(CodeScreen screen)
        {
            NewPasswordContentControl.Content = screen;
        }

        // Switch to the mail entry screen
        public void ShowMailScreen(MailScreen screen)
        {
            NewPasswordContentControl.Content = screen;
        }

        // Switch to the new password entry screen
        public void ShowNewPasswordScreen(NewPassword newPassword)
        {
            NewPasswordContentControl.Content = newPassword;
        }
    }
}
