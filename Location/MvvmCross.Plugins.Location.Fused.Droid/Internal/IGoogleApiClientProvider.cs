using System.Threading.Tasks;
using Android.Gms.Common.Apis;

namespace MvvmCross.Plugins.Location.Fused.Droid.Internal
{
    public interface IGoogleApiClientProvider
    {
        GoogleApiClient ApiClient { get; }

        Task EnsureApiCilentConnected();
    }
}