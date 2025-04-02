namespace car_rental_system_api.ViewModel
{
    public class BookingViewModel
    {
        public string BookingNo { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string CustName { get; set; } = string.Empty;
        public string CustEmail { get; set; } = string.Empty;
        public string CustPhone { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
