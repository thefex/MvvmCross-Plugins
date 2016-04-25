using Android.Content;

namespace MvvmCross.Plugins.Location.Fused.Droid.Internal
{
    internal class FusedLocationWatcherServices
    {

        public LocationAvailabilityCheck LocationAvailabilityCheck { get; private set; }

        /// <summary>
        /// It is necessary to ensure that Google Api Client it is possible to pass ApiClient as external dependency
        /// Api Client may use GoogleApiClient in multiple places!
        /// </summary>
        public IGoogleApiClientProvider GoogleApiClientProivder { get; private set; }

        public Context AndroidContext { get; private set; }
    }
}