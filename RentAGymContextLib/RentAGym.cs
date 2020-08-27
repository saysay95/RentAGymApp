using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shared
{
    public partial class RentAGym : DbContext
    {
        public RentAGym()
        {
        }

        public RentAGym(DbContextOptions<RentAGym> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingCalendar> BookingsCalendar { get; set; }
        public virtual DbSet<DurationUnit> DurationUnits { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<SpaceType> SpaceTypes { get; set; }
        public virtual DbSet<SpaceWithType> SpacesWithType { get; set; }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=RentAGym.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.HasIndex(e => e.City)
                    .HasName("CityAddresses");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCodeAddresses");

                entity.Property(e => e.AddressId)
                    .HasColumnName("AddressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.City).HasColumnType("nvarchar (15)");

                entity.Property(e => e.Country).HasColumnType("nvarchar (15)");

                entity.Property(e => e.Phone).HasColumnType("nvarchar (24)");

                entity.Property(e => e.PostalCode).HasColumnType("nvarchar (10)");

                entity.Property(e => e.Region).HasColumnType("nvarchar (15)");

                entity.Property(e => e.Street).HasColumnType("nvarchar (60)");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId)
                    .HasColumnName("BookingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookingCalendar>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.EndTime).IsRequired();

                entity.Property(e => e.StartTime).IsRequired();

                entity.HasOne(d => d.Booking)
                    .WithMany()
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DurationUnit>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnType("nvarchar(24)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnType("nvarchar(6)");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.FilePath });

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.FilePath).HasColumnType("nvarchar(260)");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DurationUnit)
                    .IsRequired()
                    .HasColumnType("nvarchar(6)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany()
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SpaceType>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.Property(e => e.Type).HasColumnType("nvarchar(6)");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnType("nvarchar(30)");
            });

            modelBuilder.Entity<SpaceWithType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId)
                    .HasColumnName("TypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SpaceId).HasColumnName("SpaceID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("nvarchar(6)");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.SpaceWithTypes)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Space>(entity =>
            {
                entity.HasKey(e => e.SpaceId);

                entity.Property(e => e.SpaceId)
                    .HasColumnName("SpaceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");

                entity.Property(e => e.HasSpaceToRent)
                    .IsRequired()
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar (30)");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Phone).HasColumnType("nvarchar(24)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Spaces)
                    .HasForeignKey(d => d.AddressId);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Spaces)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nvarchar(100)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar (30)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar (30)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(c => c.CityId);

                entity.Property(c => c.CityId)
                    .HasColumnName("CityID")
                    .ValueGeneratedNever();

                entity.Property(c => c.Name)
                    .HasColumnType("nvarchar(255)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
