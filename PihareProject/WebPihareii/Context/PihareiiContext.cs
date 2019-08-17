using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebPihareii.Entities;

namespace WebPihareii.Context
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
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<VisitState> VisitState { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
               
                entity.HasOne(d => d.Commisioner)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CommisionerId)
                    .HasConstraintName("FK_Client_Commisioner_CommisionerId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Commisioner>(entity =>
            {

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Commisioner)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Commisioner_Role_RoleId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Department>(entity =>
            {

                entity.HasOne(d => d.DepartmentState)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.DepartmentStateId)
                    .HasConstraintName("FK_Department_Departmentstate_DepartmentStateId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DepartmentType)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.DepartmentTypeId)
                    .HasConstraintName("FK_Department_Departmenttype_DepartmentTypeId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Visitregistration>(entity =>
            {

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Visitregistration_Client_ClientId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Commisioner)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.CommisionerId)
                    .HasConstraintName("FK_Visitregistration_Commisioner_CommisionerId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Visitregistration_Department_DepartmentId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.StateVisitState)
                    .WithMany(p => p.Visitregistration)
                    .HasForeignKey(d => d.StateVisitStateId)
                    .HasConstraintName("FK_Visitregistration_VisitState_StateVisitStateId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }

    }
}
