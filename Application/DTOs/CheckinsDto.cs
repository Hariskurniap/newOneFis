namespace Application.DTOs
{
    public class CheckinsDto
    {
        public string? AmtEmployeeId { get; set; }
        public string AmtEmployeeName { get; set; }
        public double CheckinLatitude { get; set; }
        public double CheckinLongitude { get; set; }
        public string CheckinDate { get; set; }
        public bool UsedInPretrip { get; set; }
        public bool UsedInDcu { get; set; }
        public string PlantCode { get; set; }
        public string CheckoutDate { get; set; }
        public string CheckoutLatitude { get; set; }
        public string CheckoutLongitude { get; set; }
        public string WorkHours { get; set; }

        // Tambahkan ini:
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
