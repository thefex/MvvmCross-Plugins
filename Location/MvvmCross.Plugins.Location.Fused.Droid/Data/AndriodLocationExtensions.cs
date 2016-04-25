using Android.OS;

namespace MvvmCross.Plugins.Location.Fused.Droid.Data
{
    public static class AndriodLocationExtensions
    {
        public static bool IsMockedLocation(this Android.Locations.Location location)
        {
            if ((int)Build.VERSION.SdkInt >= 18) // API LEVEL 18+ only!
            {
                if (location.IsFromMockProvider)
                    return true;
            }

            return false;
        }
    }
}