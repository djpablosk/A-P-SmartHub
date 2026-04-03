using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
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


namespace A_P_SmartHub.Graphics.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            // 2. Ak sme ho našli, povieme mu, nech spustí SVOJU funkciu na prechod
            if (mainWindow != null)
            {
                // Tu musíme možno pridať plnú cestu k Registru, ak je v inom priečinku. 
                // Ak ti podčiarkne slovo Register, napíš: new Registration.Register()
                mainWindow.SlideViewTransition(new Register(), true);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // nic nepridavat
        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.MainDisplay.Content = new MainDashboard();
            }

        }
    }
}
