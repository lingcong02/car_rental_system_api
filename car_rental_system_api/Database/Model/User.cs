using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace car_rental_system_api.Database.Entity
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid Guid { get; set; }
        public byte[] Hash { get; set; } = Array.Empty<byte>();
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
