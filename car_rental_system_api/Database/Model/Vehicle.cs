using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace car_rental_system_api.Database.Entity
{
    public partial class Vehicle
    {
        public int VehicleId { get; set; }
        public int FkVehicleModelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PlatNo { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual VehicleModel VehicleModel { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
