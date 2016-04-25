using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Plugins.Location.Asynchronous;
using MvvmCross.Plugins.Location.Fused.Droid.Data;
using MvvmCross.Plugins.Location.Fused.Droid.Internal;
using LocationLayerResponse = MvvmCross.Plugins.Location.Asynchronous.Data.LocationLayerResponse;

namespace MvvmCross.Plugins.Location.Fused.Droid.Async
{
    class MvxFusedAsyncLocationWatcher : MvxBaseAsyncLocationWatcher
    {
        private readonly FusedLocationWatcherServices _fusedLocationWatcherServices;

        public MvxFusedAsyncLocationWatcher(FusedLocationWatcherServices fusedLocationWatcherServices)
        {
            _fusedLocationWatcherServices = fusedLocationWatcherServices;
        }


        protected override async Task<LocationLayerResponse> StartObservingLocationChanges()
        {
            var googlePlayServicesAvailabilityResponse = await EnsureGooglePlayServicesAvailabilie();

            if (!googlePlayServicesAvailabilityResponse.IsSuccess)
                return googlePlayServicesAvailabilityResponse;

            var permissionGrantedResponse = await EnsurePermissionsGranted();

            if (!permissionGrantedResponse.IsSuccess)
                return new LocationLayerResponse().AddErrorMessage("Location permissions are not granted.");

            await _fusedLocationWatcherServices.GoogleApiClientProivder.EnsureApiCilentConnected().ConfigureAwait(false);

            // TODO Build location request and validate that.
         //   await _fusedLocationWatcherServices.LocationAvailabilityCheck.EnsureLocationRequestAvailable

            return new LocationLayerResponse() {IsSuccess = true};
        }

        private async Task<LocationLayerResponse> EnsurePermissionsGranted()
        {
            var permissionGrantedResponse =
                await _fusedLocationWatcherServices
                    .LocationAvailabilityCheck
                    .EnsureLocationPermissionsGranted()
                    .ConfigureAwait(false);
            return permissionGrantedResponse;
        }

        private async Task<LocationLayerResponse> EnsureGooglePlayServicesAvailabilie()
        {
            // TODO: pass resolution request code as dependency - configuration to prevent resultion request codes duplications inside app
            var errorResolutionRequestCode = 1234;
            var googlePlayServicesAvailabilityResponse =
                await _fusedLocationWatcherServices
                    .LocationAvailabilityCheck
                    .IsGooglePlayServicesAvailabile(_fusedLocationWatcherServices.AndroidContext, errorResolutionRequestCode)
                    .ConfigureAwait(false);
            return googlePlayServicesAvailabilityResponse;
        }

        protected override Task<LocationLayerResponse> StopObservingLocationChanges()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> IsLocationAvailable()
        {
            throw new NotImplementedException();
        }
    }
}