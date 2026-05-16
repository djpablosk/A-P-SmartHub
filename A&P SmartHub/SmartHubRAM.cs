using A_P_SmartHub.Type_devices_with_graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace A_P_SmartHub
{
    public static class SmartHubRAM
    {
        //climate
        public static double SavecurrentTemperatureClimate = 25.0;
        public static string SavecurrentstatClimate = "AUTO";

        //light
        public static double SavecurrentBrightnessLight = 0;

        //media
        public static double SavecurrentVolumeLevel = 0;

        //mainDashboard
        public static ObservableCollection<DeviceType> RecentDevices { get; set; } = new ObservableCollection<DeviceType>();
    }
}
