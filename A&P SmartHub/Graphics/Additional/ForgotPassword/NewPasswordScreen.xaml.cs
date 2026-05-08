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
            // Show initial screen (mail entry) when this control is constructed
            passwordnewControl.Content = new MailScreen();
        }

        // Show the code entry screen
        // Called by child screens to switch the displayed content to the code entry screen
        public void ShowCodeScreen(CodeScreen screen)
        {
            passwordnewControl.Content = screen;
            // Ensure the content control for the newly shown screen hosts the provided screen
            // If the XAML defines a second content control named NewPasswordContentControl, set it.
            var field = this.GetType().GetField("NewPasswordContentControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (field != null)
            {
                var control = field.GetValue(this) as ContentControl;
                if (control != null)
                    control.Content = screen;
            }
        }

        // Switch to the mail entry screen
        public void ShowMailScreen(MailScreen screen)
        {
            var field = this.GetType().GetField("NewPasswordContentControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (field != null)
            {
                var control = field.GetValue(this) as ContentControl;
                if (control != null)
                    control.Content = screen;
            }
        }

        // Show the new password screen
        // Switch to the new password entry screen
        public void ShowNewPasswordScreen(NewPassword newPassword)
        {
            passwordnewControl.Content = newPassword;
            var field = this.GetType().GetField("NewPasswordContentControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (field != null)
            {
                var control = field.GetValue(this) as ContentControl;
                if (control != null)
                    control.Content = newPassword;
            }
        }
    }
}
