using System;

namespace MvvmCross.Plugins.Location.Fused.Droid.Data
{
    public static class MvxGeoLocationExtensions
    {
        public static bool IsMockedLocation(this MvxGeoLocation location)
        {
            var mvxAndroidGeoLocation = location as MvxAndroidGeoLocation;

            if (mvxAndroidGeoLocation == null)
                throw new InvalidOperationException("MvxGeoLocation is not MvxAndroidGeoLocation. Only MvxFusedAsyncLocationWatcher support mock-location detection atm.");

            return mvxAndroidGeoLocation.IsMockedLocation;
        }
    }
}