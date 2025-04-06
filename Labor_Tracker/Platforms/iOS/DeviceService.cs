using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Labor_Tracker.Services;

namespace Labor_Tracker.Platforms.iOS
{
    public class DeviceService : IDeviceService
    {
        public string GetDeviceName()
        {
            return UIDevice.CurrentDevice.Name;
        }
    }
}
