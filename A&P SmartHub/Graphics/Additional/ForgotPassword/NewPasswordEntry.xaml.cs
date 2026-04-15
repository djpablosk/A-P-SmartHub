using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace A_P_SmartHub.Graphics.Additional.ForgotPassword
{
    /// <summary>
    /// Interaction logic for NewPasswordEntry.xaml
    /// </summary>
    public partial class NewPasswordEntry : UserControl
    {
        public NewPasswordEntry()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = this;
            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent is NewPasswordScreen newPasswordScreen)
                {
                    newPasswordScreen.ShowMailScreen();
                    break;
                }
            }
        }

        private void SetPassword_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implement password validation and submission
        }
    }
}
