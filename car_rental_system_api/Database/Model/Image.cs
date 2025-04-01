using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace car_rental_system_api.Database.Entity
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int FkVehicleId { get; set; }
        public string? Path { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
