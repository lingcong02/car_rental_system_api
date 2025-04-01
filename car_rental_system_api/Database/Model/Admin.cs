using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace car_rental_system_api.Database.Entity
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Role { get; set; }
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public Guid Guid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
