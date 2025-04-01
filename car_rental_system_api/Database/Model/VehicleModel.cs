using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace car_rental_system_api.Database.Entity
{
    public partial class VehicleModel
    {
        public int VehicleModelId { get; set; }
        public string Desc { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
