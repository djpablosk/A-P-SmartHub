using System;
using System.Collections.Generic;
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
    }
}
