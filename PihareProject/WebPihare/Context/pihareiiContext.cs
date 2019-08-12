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
        public virtual DbSet<Role> Role { get; set; }
      
    }
}
