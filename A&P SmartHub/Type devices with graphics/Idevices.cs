using A_P_SmartHub.Graphics.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace A_P_SmartHub.Type_devices_with_graphics
{
    
    

        public enum DeviceTypeEnum
        {
            Lights,           //vsetko co sa tyka osvetlenia
            Toggles,         //vypinace zasuvky predlzovacky 
            Climates,       //aj termostaty aj hlavice a ostatne
            Covers,         //zaluzie, roletky, brany, garazove brany
            Media,          //televizory, audio systemy, prehravace
            Readonly     //vsetko co sa da len citat senzory kamery teplomery a adt
        }

        public class DeviceType
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DeviceTypeEnum Type { get; set; }

        }
    
}
