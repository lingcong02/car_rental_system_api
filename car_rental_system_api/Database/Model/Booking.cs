using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace car_rental_system_api.Database.Entity
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int FkVehicleId { get; set; }
        public int FkUserId { get; set; }
        public string BookingNo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
