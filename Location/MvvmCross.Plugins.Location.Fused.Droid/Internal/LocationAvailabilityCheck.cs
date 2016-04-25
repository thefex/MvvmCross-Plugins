using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Location.Asynchronous.Data;
using MvvmCross.Plugins.Location.Fused.Droid.ErrorHandling;

namespace MvvmCross.Plugins.Location.Fused.Droid.Internal
{
    internal class LocationAvailabilityCheck
    {
        private readonly IMvxAndroidCurrentTopActivity _topActivityProvider;
        private readonly IConncetionResultErrorResolver _connetionResultErrorResolver;
        private readonly IStatusesErrorResolver _statusesErrorResolver;

        public LocationAvailabilityCheck(IMvxAndroidCurrentTopActivity topActivityProvider, IConncetionResultErrorResolver connetionResultErrorResolver, IStatusesErrorResolver statusesErrorResolver)
        {
            _topActivityProvider = topActivityProvider;
            _connetionResultErrorResolver = connetionResultErrorResolver;
            _statusesErrorResolver = statusesErrorResolver;
        }

        public async Task<LocationLayerResponse> EnsureLocationPermissionsGranted()
        {
            // TODO
            // MARSHMALLOW ONLY
            // Use Cheesebaron! Permission Plugin which isn't officialy published yet!
            // TODO
            return new LocationLayerResponse() {IsSuccess = true};
        }

        public async Task<LocationLayerResponse> IsGooglePlayServicesAvailabile(Context context, int errorResolutionRequestCode)
        {
            int googleApiAvailabilityResponse = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);

            if (googleApiAvailabilityResponse != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(googleApiAvailabilityResponse))
                {
                    return await TryResolveGooglePlayAvailabilityError(context, errorResolutionRequestCode, googleApiAvailabilityResponse);
                }
                else
                {
                    var errorDialog = GoogleApiAvailability.Instance.GetErrorDialog(_topActivityProvider.Activity, googleApiAvailabilityResponse, errorResolutionRequestCode);
                    await _connetionResultErrorResolver.HandleUnresorvableError(errorDialog);
                    return new LocationLayerResponse() {IsSuccess = false};
                }
            }

            return new LocationLayerResponse() {IsSuccess = true};
        }

        private async Task<LocationLayerResponse> TryResolveGooglePlayAvailabilityError(Context context, int errorResolutionRequestCode,
            int googleApiAvailabilityResponse)
        {
            var pendingIntent = GoogleApiAvailability.Instance.GetErrorResolutionPendingIntent(context,
                googleApiAvailabilityResponse, errorResolutionRequestCode);
            var errorResolveConnectionResult = new ConnectionResult(googleApiAvailabilityResponse, pendingIntent);

            return new LocationLayerResponse()
            {
                IsSuccess = await _connetionResultErrorResolver.ResolveErrors(errorResolveConnectionResult)
            };
        }

        public async Task<LocationLayerResponse> EnsureLocationRequestAvailable(GoogleApiClient apiClient,
            LocationRequest locationRequest, int error)
        {
            var locationSettingsRequest = new LocationSettingsRequest.Builder()
             .AddLocationRequest(locationRequest)
             .SetAlwaysShow(true)
             .Build();

            var locationSettingsResponse = await LocationServices.SettingsApi.CheckLocationSettingsAsync(apiClient, locationSettingsRequest);

            if (!locationSettingsResponse.Status.IsSuccess)
            {
                bool hasErrorResolvingSucceed = await _statusesErrorResolver.TryResolveErrors(locationSettingsResponse.Status);

                return new LocationLayerResponse() {IsSuccess = hasErrorResolvingSucceed};
            }

            return new LocationLayerResponse() {IsSuccess = true};
        }
    }
}