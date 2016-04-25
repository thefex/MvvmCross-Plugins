namespace MvvmCross.Plugins.Location.Fused.Droid.Data
{
    // leaky abstraction to give user ability to detect mock locations.
    public class MvxAndroidGeoLocation : MvxGeoLocation
    {
        public bool IsMockedLocation { get; internal set; }
    }
}