using System.Collections.Generic;
using System.Linq;

namespace MvvmCross.Plugins.Location.Asynchronous.Data
{
    public class LocationLayerResponse
    {
        private readonly IList<string> _errorMessages = new List<string>();
        private bool isSuccess = false;

        public LocationLayerResponse()
        {
                
        }

        public bool IsSuccess
        {
            get { return isSuccess && !_errorMessages.Any(); }
            set { isSuccess = value; }
        }

        public string ErrorMessage
            => _errorMessages.Any() ? _errorMessages.Aggregate((prev, current) => prev + "\n" + current) : string.Empty;

        public LocationLayerResponse AddErrorMessage(string errorMessage)
        {
            _errorMessages.Add(errorMessage);
            IsSuccess = false;

            return this;
        }
    }
}