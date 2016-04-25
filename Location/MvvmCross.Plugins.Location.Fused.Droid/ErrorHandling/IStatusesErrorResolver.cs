using System.Threading.Tasks;
using Android.Gms.Common.Apis;

namespace MvvmCross.Plugins.Location.Fused.Droid.ErrorHandling
{
    public interface IStatusesErrorResolver
    {
        Task<bool> TryResolveErrors(Statuses statuses);
    }
}