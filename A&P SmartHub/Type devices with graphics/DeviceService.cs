using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A_P_SmartHub.Type_devices_with_graphics
{
    internal class DeviceService
    {
        public async Task<List<Idevices.DeviceType>> GetDevicesAsync()
        {
            // Simulate loading data - replace with DB or API calls as needed
            await Task.Delay(1000);

            return new List<Idevices.DeviceType>
            {
                new Idevices.DeviceType { ID = 1, Name = "Televize", Type = Idevices.DeviceTypeEnum.Media },
                new Idevices.DeviceType { ID = 2, Name = "Lednička", Type = Idevices.DeviceTypeEnum.Toggles },
                new Idevices.DeviceType { ID = 3, Name = "Kávovar", Type = Idevices.DeviceTypeEnum.Toggles }
            };
        }
    }
}
