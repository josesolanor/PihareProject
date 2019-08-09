﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebPihare.Context;

namespace WebPihare.Migrations
{
    [DbContext(typeof(PihareiiContext))]
    [Migration("20190809041513_registerAndClient")]
    partial class registerAndClient
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebPihare.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CI");

                    b.Property<int?>("CommisionerId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Observation");

                    b.Property<DateTime>("RegistredDate");

                    b.Property<string>("SecondLastName");

                    b.Property<int>("Telefono");

                    b.HasKey("ClientId");

                    b.HasIndex("CommisionerId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("WebPihare.Entities.Commisioner", b =>
                {
                    b.Property<int>("CommisionerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommisionerPassword");

                    b.Property<int>("ContractNumber");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Nic");

                    b.Property<int>("RoleId");

                    b.Property<string>("SecondLastName");

                    b.Property<int>("Telefono");

                    b.HasKey("CommisionerId");

                    b.HasIndex("RoleId");

                    b.ToTable("Commisioner");
                });

            modelBuilder.Entity("WebPihare.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DeparmentPrice");

                    b.Property<int>("DepartmentCode");

                    b.Property<string>("DepartmentDescription");

                    b.Property<int>("DepartmentStateId");

                    b.Property<int>("DepartmentTypeId");

                    b.Property<int>("NumberBedrooms");

                    b.Property<int>("NumberFloor");

                    b.HasKey("DepartmentId");

                    b.HasIndex("DepartmentStateId");

                    b.HasIndex("DepartmentTypeId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("WebPihare.Entities.Departmentstate", b =>
                {
                    b.Property<int>("DepartmentStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DepartmentStateDescription");

                    b.Property<string>("DepartmentStateValue");

                    b.HasKey("DepartmentStateId");

                    b.ToTable("Departmentstate");
                });

            modelBuilder.Entity("WebPihare.Entities.Departmenttype", b =>
                {
                    b.Property<int>("DepartmentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DepartmentTypeDescription");

                    b.Property<string>("DepartmentTypeValue");

                    b.HasKey("DepartmentTypeId");

                    b.ToTable("Departmenttype");
                });

            modelBuilder.Entity("WebPihare.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleDescription");

                    b.Property<string>("RoleValue");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("WebPihare.Entities.Visitregistration", b =>
                {
                    b.Property<int>("VisitRegistrationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int>("CommisionerId");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Observations");

                    b.Property<DateTime>("VisitDay");

                    b.HasKey("VisitRegistrationId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CommisionerId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Visitregistration");
                });

            modelBuilder.Entity("WebPihare.Entities.Client", b =>
                {
                    b.HasOne("WebPihare.Entities.Commisioner", "Commisioner")
                        .WithMany()
                        .HasForeignKey("CommisionerId");
                });

            modelBuilder.Entity("WebPihare.Entities.Commisioner", b =>
                {
                    b.HasOne("WebPihare.Entities.Role", "Role")
                        .WithMany("Commisioner")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebPihare.Entities.Department", b =>
                {
                    b.HasOne("WebPihare.Entities.Departmentstate", "DepartmentState")
                        .WithMany("Department")
                        .HasForeignKey("DepartmentStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebPihare.Entities.Departmenttype", "DepartmentType")
                        .WithMany("Department")
                        .HasForeignKey("DepartmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebPihare.Entities.Visitregistration", b =>
                {
                    b.HasOne("WebPihare.Entities.Client", "Client")
                        .WithMany("Visitregistration")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebPihare.Entities.Commisioner", "Commisioner")
                        .WithMany("Visitregistration")
                        .HasForeignKey("CommisionerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebPihare.Entities.Department", "Department")
                        .WithMany("Visitregistration")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
