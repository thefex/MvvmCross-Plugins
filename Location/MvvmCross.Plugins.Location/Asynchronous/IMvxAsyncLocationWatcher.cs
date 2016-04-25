using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Plugins.Location.Asynchronous.Data;

namespace MvvmCross.Plugins.Location.Asynchronous
{
    public interface IMvxAsyncLocationWatcher
    {
        /// <summary>
        /// Starts observing for Location Changes.
        /// </summary>
        /// <returns></returns>

        Task<LocationLayerResponse> Start();

        /// <summary>
        /// Stops observing for Location Changes.
        /// </summary>
        /// <returns></returns>
        Task<LocationLayerResponse> Stop();

        /// <summary>
        /// If LocationWatcher is running this property shall return true.
        /// </summary>
        bool IsObservingLocationChanges { get; }

        /// <summary>
        /// Returns most recents GeoLocation. This task won't complete until any location is provided.
        /// </summary>
        /// <returns></returns>
        Task<MvxGeoLocation> GetMostRecentLocation();

        Task<bool> IsLocationAvailable();

        /// <summary>
        /// Event should be fired when new GeoLocation is provided.
        /// </summary>
        event Action<MvxGeoLocation> GeolocationChanged;
    }
}
