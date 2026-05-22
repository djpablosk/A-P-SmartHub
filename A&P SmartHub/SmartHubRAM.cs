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
        public static double SavecurrentTemperatureClimate {  get; set; } = 25.0;
        public static string SavecurrentstatClimate = "AUTO";

        //light
        public static double SavecurrentBrightnessLight {  get; set; }

        //media
        public static double SavecurrentVolumeLevel { get; set; }
        public static string spotifyAcceskey {  get; set; }
        public static string SpotifyRefreshKey {  get; set; }

        //mainDashboard
        public static ObservableCollection<DeviceType> RecentDevices { get; set; } = new ObservableCollection<DeviceType>();
    }
}
