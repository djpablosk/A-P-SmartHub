using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;

namespace A_P_SmartHub.Graphics.Additional
{
    public partial class NoWifiScreen : UserControl
    {
        public NoWifiScreen()
        {
            InitializeComponent();
            Loaded += (s, e) => FullWifi();  // ai nam pomohlo s tymto trosku
        }

        public bool WifiOn()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Any(n => ( n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                       && n.OperationalStatus == OperationalStatus.Up);
        }

        public void FullWifi()
        {
            if (WifiOn())
            {
                OfflineOverlay.Visibility = Visibility.Hidden;

                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.SlideViewTransition(
                        new A_P_SmartHub.Graphics.Login.Login(), true);
                }
            }
            else
            {
                OfflineOverlay.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FullWifi();
        }
    }
}