using A_P_SmartHub.Graphics.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace A_P_SmartHub.Type_devices_with_graphics
{
    
    

        public enum DeviceTypeEnum
        {
            Light,           //vsetko co sa tyka osvetlenia
            Toggle,         //vypinace zasuvky predlzovacky 
            Climate,       //aj termostaty aj hlavice a ostatne
            Cover,         //zaluzie, roletky, brany, garazove brany
            Media         //televizory, audio systemy, prehravace
           
        }

        public class DeviceType
        {
            public string IpAddress { get; set; }
            public string DeviceName { get; set; }
            public DeviceTypeEnum Type { get; set; }

        }
    
}
