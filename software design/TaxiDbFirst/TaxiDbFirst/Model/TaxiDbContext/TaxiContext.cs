using Microsoft.EntityFrameworkCore;

namespace TaxiDbFirst.Model.TaxiDbContext;

public partial class TaxiContext : DbContext
{
    public TaxiContext()
    {
    }

    public TaxiContext(DbContextOptions<TaxiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverQualification> DriverQualifications { get; set; }

    public virtual DbSet<NewDriver> NewDrivers { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripStatus> TripStatuses { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Taxi;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F3340622C");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FD8FFE0D0");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Customer__4849DA013086A30A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Customer__AB6E6164FA6A1CD8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientFrom).HasColumnName("clientFrom");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3213E83FD57B5455");

            entity.ToTable("Driver");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Driver__4849DA012EA75696").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Driver__AB6E6164D0EB806A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.StartedWorkingOn).HasColumnName("startedWorkingOn");
        });

        modelBuilder.Entity<DriverQualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DriverQu__3213E83FDF01D026");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.DriverId).HasColumnName("driverId");

            entity.HasOne(d => d.Category).WithMany(p => p.DriverQualifications)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__DriverQua__categ__44FF419A");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverQualifications)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__DriverQua__drive__440B1D61");
        });

        modelBuilder.Entity<NewDriver>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NewDriver");

            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.StartedWorkingOn).HasColumnName("startedWorkingOn");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Point__3213E83F4A55DDC5");

            entity.ToTable("Point");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .HasColumnName("number");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trip__3213E83FC6A889E1");

            entity.ToTable("Trip");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.DriverId).HasColumnName("driverId");
            entity.Property(e => e.EndPointId).HasColumnName("endPointId");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .HasColumnName("number");
            entity.Property(e => e.PickUpTime).HasColumnName("pickUpTime");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.StartPointId).HasColumnName("startPointId");
            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Trips)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__customerId__534D60F1");

            entity.HasOne(d => d.Driver).WithMany(p => p.Trips)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__driverId__52593CB8");

            entity.HasOne(d => d.EndPoint).WithMany(p => p.TripEndPoints)
                .HasForeignKey(d => d.EndPointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__endPointId__5629CD9C");

            entity.HasOne(d => d.StartPoint).WithMany(p => p.TripStartPoints)
                .HasForeignKey(d => d.StartPointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__startPoint__5535A963");

            entity.HasOne(d => d.Status).WithMany(p => p.Trips)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__statusId__5441852A");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Trips)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trip__vehicleId__5165187F");
        });

        modelBuilder.Entity<TripStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TripStat__3213E83F9F73DAA3");

            entity.ToTable("TripStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3213E83F34FCBEAC");

            entity.ToTable("Vehicle");

            entity.HasIndex(e => e.LicensePlate, "UQ__Vehicle__5BC9DE4188C3DDC1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(255)
                .HasColumnName("licensePlate");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(255)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasColumnName("model");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .HasColumnName("number");
            entity.Property(e => e.Seats)
                .HasDefaultValue(4)
                .HasColumnName("seats");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Category).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicle__categor__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
