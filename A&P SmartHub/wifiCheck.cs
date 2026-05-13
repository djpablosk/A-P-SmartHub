using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace A_P_SmartHub
{
    internal class wifiCheck
    {
   public  bool wifion()
        {
            return NetworkInterface.GetAllNetworkInterfaces().Any(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
            && n.OperationalStatus == OperationalStatus.Up);
        }
        public void Fullwifi()
        {
          bool isOn =  wifion();
            if (isOn)
            {
                // prepneme spat ba login
            }
            else
            {
                // prepnutie na tu blbost
            }
        }
    }
  
}
