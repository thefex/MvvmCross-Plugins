using System.Threading.Tasks;
using Android.App;
using Android.Gms.Common;

namespace MvvmCross.Plugins.Location.Fused.Droid.ErrorHandling
{
    internal interface IConncetionResultErrorResolver
    {
        Task<bool> ResolveErrors(ConnectionResult connectionResult);

        Task HandleUnresorvableError(Dialog errorDialog);
    }
}