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
            
            passwordnewControl.Content = new MailScreen();
        }

      
        public void ShowCodeScreen(CodeScreen screen)
        {
            passwordnewControl.Content = screen;
            var field = this.GetType().GetField("NewPasswordContentControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (field != null)
            {
                var control = field.GetValue(this) as ContentControl;
                if (control != null)
                    control.Content = screen;
            }
        }

        
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
