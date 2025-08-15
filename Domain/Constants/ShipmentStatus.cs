namespace Domain.Constants
{
    public static class ShipmentStatus
    {
        public const string STATUS_LOADING = "loading"; // purple
        public const string STATUS_GATE_OUT_VALIDATION = "gate out validation"; // yellow
        public const string STATUS_ON_DELIVERY = "on delivery"; // pink
        public const string STATUS_ON_ARRIVED = "on arrived"; // pink
        public const string STATUS_DONE = "done"; // blue
        public const string STATUS_CANCEL = "cancel"; // red
        public const string STATUS_AMT_NOT_ACCEPT = "amt not accept"; // red

        public static Dictionary<string, string> GetAllWithColor()
        {
            return new Dictionary<string, string>
            {
                { STATUS_LOADING, "purple" },
                { STATUS_GATE_OUT_VALIDATION, "yellow" },
                { STATUS_ON_DELIVERY, "pink" },
                { STATUS_ON_ARRIVED, "pink" },
                { STATUS_DONE, "blue" },
                { STATUS_CANCEL, "red" },
                { STATUS_AMT_NOT_ACCEPT, "red" },
            };
        }
    }
}
