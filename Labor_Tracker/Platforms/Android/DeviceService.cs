using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labor_Tracker.Services;
using Android.OS;

namespace Labor_Tracker.Platforms.Android
{
    public class DeviceService : IDeviceService
    {
        public string GetDeviceName()
        {
            return Build.Model;
        }
    }
}
