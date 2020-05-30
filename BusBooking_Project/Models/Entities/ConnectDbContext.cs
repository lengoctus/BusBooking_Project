using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusBooking_Project.Models.Entities
{
    public partial class ConnectDbContext : DbContext
    {
        public ConnectDbContext()
        {
        }

        public ConnectDbContext(DbContextOptions<ConnectDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Age> Age { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Spacing> Spacing { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=BusBooking;user id=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DayCreate).HasColumnType("datetime");

                entity.Property(e => e.DayEdited).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ForgotPass).HasMaxLength(50);

                entity.Property(e => e.Gender).HasDefaultValueSql("((1))");

                entity.Property(e => e.Images)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_Account_Station");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.DayCreate).HasColumnType("datetime");

                entity.Property(e => e.DayStart).HasColumnType("datetime");

                entity.Property(e => e.FromCity).HasColumnName("FromCIty");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK_Booking_Bus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookingUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Booking_Account");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.BookingUserId2Navigation)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK_Booking_Account1");

                entity.HasOne(d => d.UserId21)
                    .WithMany(p => p.InverseUserId21)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK_Booking_Booking");
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Bus)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK_Bus_Category1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK_Seat_Bus");
            });

            modelBuilder.Entity<Spacing>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.AgeId)
                    .HasConstraintName("FK_Ticket_Age");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Ticket_Booking");

                entity.HasOne(d => d.Spacing)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.SpacingId)
                    .HasConstraintName("FK_Ticket_Spacing");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
