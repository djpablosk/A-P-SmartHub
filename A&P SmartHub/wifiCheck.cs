using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Login;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;

namespace A_P_SmartHub
{
    internal class wifiCheck
    {
        // tu sme si pomohli s aikom nieco ak to citas tak zdravim
        public bool wifion()
        { 
            return NetworkInterface.GetAllNetworkInterfaces().Any(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
            && n.OperationalStatus == OperationalStatus.Up);
        }

        NoWifiScreen nowifi = new NoWifiScreen();
        Login login = new Login();

        public void Fullwifi()
        {

            //            var mainWindow = Window.GetWindow(this) as MainWindow;
            bool isOn = wifion();
            if (isOn)
            {
                //  mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
            else
            {

            }
        }
    }

}
