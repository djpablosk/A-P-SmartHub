using A_P_SmartHub.Graphics.Login;
using System;
using System.Collections.Generic;
using System.Linq; // ✅ Added - required for .Any()
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LoginScreen = A_P_SmartHub.Graphics.Login.Login; // ✅ Add this alias
using System.Windows.Shapes;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for NoWifiScreen.xaml
    /// </summary>
    public partial class NoWifiScreen : UserControl
    {
        public NoWifiScreen()
        {
            InitializeComponent();
            FullWifi();
        }

        public bool WifiOn() 
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Any(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                       && n.OperationalStatus == OperationalStatus.Up);
        }

        public void FullWifi()
        {
           
            if (Window.GetWindow(this) is not MainWindow mainWindow)
            {
                MessageBox.Show("Could not find the main window.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WifiOn())
            {
                mainWindow.SlideViewTransition(new LoginScreen(), true);
            }
            else
            {
                MessageBox.Show("No Wi-Fi connection detected. Please connect and try again.",
                    "No Connection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}