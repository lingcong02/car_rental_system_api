using car_rental_system_api.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace car_rental_system_api.Database
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }

        public virtual DbSet<Vehicle> Vehicles {  get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.HasKey(e => e.ImageId).HasName("PK_Image");

                entity.Property(e => e.ImageId)
                    .ValueGeneratedNever(); // Assuming identity is not used

                entity.Property(e => e.Path)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();

                entity.HasOne(e => e.Vehicle)
                    .WithMany(e => e.Images)
                    .HasForeignKey(e => e.FkVehicleId)
                    .OnDelete(DeleteBehavior.Cascade) // Adjust as per business logic
                    .HasConstraintName("FK_Image_Vehicle");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasKey(e => e.AdminId).HasName("PK_Admin");

                entity.Property(e => e.AdminId)
                    .ValueGeneratedNever(); // Assuming identity is not used

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.Property(e => e.Guid)
                    .IsRequired();

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasKey(e => e.BookingId).HasName("PK_Booking");

                entity.Property(e => e.BookingId)
                    .UseIdentityColumn();

                entity.Property(e => e.BookingNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.StartDate)
                    .IsRequired();

                entity.Property(e => e.EndDate)
                    .IsRequired();

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();

                entity.HasIndex(e => e.BookingNo)
                    .IsUnique()
                    .HasDatabaseName("UniqueBookingNo_Booking");

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.FkUserId)
                    .OnDelete(DeleteBehavior.Cascade) // Adjust as per business logic
                    .HasConstraintName("FK_Booking_User");

                entity.HasOne(e => e.Vehicle)
                    .WithMany()
                    .HasForeignKey(e => e.FkVehicleId)
                    .OnDelete(DeleteBehavior.Cascade) // Adjust as per business logic
                    .HasConstraintName("FK_Booking_Vehicle");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.UserId).HasName("PK_User");

                entity.Property(e => e.UserId)
                    .UseIdentityColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Guid)
                    .IsRequired();

                entity.Property(e => e.Hash)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();

                entity.HasIndex(e => e.Name)
                    .IsUnique()
                    .HasDatabaseName("UniqueUserName_User");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.HasKey(e => e.VehicleId).HasName("PK_Vehicle");

                entity.Property(e => e.VehicleId)
                    .UseIdentityColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.PlatNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Desc)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();

                entity.HasIndex(e => e.PlatNo)
                    .IsUnique()
                    .HasDatabaseName("UniquePlatNo_Vehicle");

                entity.HasOne(e => e.VehicleModel)
                    .WithMany(e => e.Vehicles)
                    .HasForeignKey(e => e.FkVehicleModelId)
                    .OnDelete(DeleteBehavior.Cascade) // Adjust as per business logic
                    .HasConstraintName("FK_Vehicle_VehicleModel");
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.ToTable("VehicleModel");

                entity.HasKey(e => e.VehicleModelId).HasName("PK_VehicleModel");

                entity.Property(e => e.VehicleModelId)
                    .UseIdentityColumn();

                entity.Property(e => e.Desc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.IsDeleted)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .IsRequired();
            });
        }
    }
}
