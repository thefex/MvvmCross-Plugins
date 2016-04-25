using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Plugins.Location.Asynchronous.Data;

namespace MvvmCross.Plugins.Location.Asynchronous
{
    /// <summary>
    /// This shall be used as base class (in most cases I can imagine) if you're implemeting IMvxAsyncLocationWatcher.
    /// It implements boiler-plate code related to initialization with ensuring thread safety.
    /// </summary>
    public abstract class MvxBaseAsyncLocationWatcher : IMvxAsyncLocationWatcher
    {
        private readonly Semaphore _threadSynchronizationSempahore = new Semaphore(1, 1);

        protected MvxBaseAsyncLocationWatcher( )
        {
            
        }

        public async Task<LocationLayerResponse> Start()
        {
            try
            {
                _threadSynchronizationSempahore.WaitOne();
                var response = await StartObservingLocationChanges().ConfigureAwait(false);
                if (response.IsSuccess)
                    IsObservingLocationChanges = true;

                return response;
            }
            finally
            {
                _threadSynchronizationSempahore.Release();
            }
        }

        protected abstract Task<LocationLayerResponse> StartObservingLocationChanges();

        public async Task<LocationLayerResponse> Stop()
        {
            try
            {
                _threadSynchronizationSempahore.WaitOne();
                var response = await StopObservingLocationChanges().ConfigureAwait(false);
                if (response.IsSuccess)
                    IsObservingLocationChanges = false;

                return response;
            }
            finally
            {
                _threadSynchronizationSempahore.Release();
            }
        }

        protected abstract Task<LocationLayerResponse> StopObservingLocationChanges();

        public bool IsObservingLocationChanges { get; private set; }
        public Task<MvxGeoLocation> GetMostRecentLocation()
        {
            throw new NotImplementedException();
        }

        public abstract Task<bool> IsLocationAvailable();

        public event Action<MvxGeoLocation> GeolocationChanged;

        protected virtual void OnGeolocationChanged(MvxGeoLocation obj)
        {
            GeolocationChanged?.Invoke(obj);
        }
    }
}
