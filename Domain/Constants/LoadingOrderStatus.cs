using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class LoadingOrderStatus
    {
        public const int OPEN = 0;
        public const int DISPATCH_VALIDATION = 1;
        public const int GET_IN = 2;
        public const int LOADING = 3;
        public const int LOADED = 4;
        public const int GET_OUT = 5;
        public const int DELIVERY = 6;
        public const int ARRIVED = 7;
        public const int END_SHIPMENT = 8;
        public const int REJECT_DELETE_ON_SAP = 9;

        public static Dictionary<int, string> GetAll()
        {
            return new Dictionary<int, string>
            {
                { OPEN, "Open" },
                { DISPATCH_VALIDATION, "Dispatch validation" },
                { GET_IN, "Get In" },
                { LOADING, "Loading" },
                { LOADED, "Loaded" },
                { GET_OUT, "Get Out" },
                { DELIVERY, "Delivery" },
                { ARRIVED, "Arrived" },
                { END_SHIPMENT, "End Shipment" },
                { REJECT_DELETE_ON_SAP, "Reject/Delete On SAP" }
            };
        }

        public static string? GetName(int statusCode)
        {
            return GetAll().TryGetValue(statusCode, out var name) ? name : null;
        }
    }
}
