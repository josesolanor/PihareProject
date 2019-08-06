using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebPihare.Entities;

namespace WebPihare.Context
{
    public partial class PihareiiContext : DbContext
    {
        public PihareiiContext()
        {
        }

        public PihareiiContext(DbContextOptions<PihareiiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Commisioner> Commisioner { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Departmentstate> Departmentstate { get; set; }
        public virtual DbSet<Departmenttype> Departmenttype { get; set; }
        public virtual DbSet<Visitregistration> Visitregistration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;database=pihareii;user=root;password=P@ssw0rd94*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "pihareii");

                entity.Property(e => e.ClientId).HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Observation)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SecondLastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Commisioner>(entity =>
            {
                entity.ToTable("commisioner", "pihareii");

                entity.Property(e => e.CommisionerId).HasColumnType("int(11)");

                entity.Property(e => e.CommisionerPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContractNumber).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nic)
                    .IsRequired()
                    .HasColumnName("NIC")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SecondLastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department", "pihareii");

                entity.HasIndex(e => e.DepartmentStateId)
                    .HasName("DepartmentStateId");

                entity.HasIndex(e => e.DepartmentTypeId)
                    .HasName("DepartmentTypeId");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.DeparmentPrice).HasColumnType("decimal(10,0)");

                entity.Property(e => e.DepartmentCode).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentStateId).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentTypeId).HasColumnType("int(11)");

                entity.Property(e => e.NumberBedrooms).HasColumnType("int(11)");

                entity.Property(e => e.NumberFloor).HasColumnType("int(11)");

                entity.HasOne(d => d.DepartmentState)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.DepartmentStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_ibfk_2");

                entity.HasOne(d => d.DepartmentType)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.DepartmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_ibfk_1");
            });

            modelBuilder.Entity<Departmentstate>(entity =>
            {
                entity.ToTable("departmentstate", "pihareii");

                entity.Property(e => e.DepartmentStateId).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentStateDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentStateValue)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departmenttype>(entity =>
            {
                entity.ToTable("departmenttype", "pihareii");

                entity.Property(e => e.DepartmentTypeId).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentTypeDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentTypeValue)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visitregistration>(entity =>
            {
                entity.ToTable("visitregistration", "pihareii");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.HasIndex(e => e.CommisionerId)
                    .HasName("CommisionerId");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("DepartmentId");

                entity.Property(e => e.VisitRegistrationId).HasColumnType("int(11)");

                entity.Property(e => e.ClientId).HasColumnType("int(11)");

                entity.Property(e => e.CommisionerId).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.Observations)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ReferencialPrice).HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visitregistration_ibfk_1");

                entity.HasOne(d => d.Commisioner)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.CommisionerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visitregistration_ibfk_3");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visitregistration_ibfk_2");
            });
        }
    }
}
