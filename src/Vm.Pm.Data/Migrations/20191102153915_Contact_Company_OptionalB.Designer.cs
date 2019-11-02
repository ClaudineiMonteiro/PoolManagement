﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vm.Pm.Data.Context;

namespace Vm.Pm.Data.Migrations
{
    [DbContext(typeof(PoolManagementDbContext))]
    [Migration("20191102153915_Contact_Company_OptionalB")]
    partial class Contact_Company_OptionalB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vm.Pm.Business.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Apt_Suite_Unit")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PublicPlace")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("State_Province")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TypeAddress")
                        .HasColumnType("int");

                    b.Property<string>("ZipPostalCode")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Collaborator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("FEIEIN")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LegalName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TradeName")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypePhone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Address", b =>
                {
                    b.HasOne("Vm.Pm.Business.Models.Collaborator", "Collaborator")
                        .WithMany("Addresses")
                        .HasForeignKey("CollaboratorId");

                    b.HasOne("Vm.Pm.Business.Models.Company", "Company")
                        .WithMany("Addresses")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Vm.Pm.Business.Models.Contact", "Contact")
                        .WithMany("Adresses")
                        .HasForeignKey("ContactId");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Collaborator", b =>
                {
                    b.HasOne("Vm.Pm.Business.Models.Company", "Company")
                        .WithMany("Collaborators")
                        .HasForeignKey("CompanyId")
                        .IsRequired();
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Contact", b =>
                {
                    b.HasOne("Vm.Pm.Business.Models.Collaborator", "Collaborator")
                        .WithMany("Contacts")
                        .HasForeignKey("CollaboratorId");

                    b.HasOne("Vm.Pm.Business.Models.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Vm.Pm.Business.Models.Phone", b =>
                {
                    b.HasOne("Vm.Pm.Business.Models.Collaborator", "Collaborator")
                        .WithMany("Phones")
                        .HasForeignKey("CollaboratorId");

                    b.HasOne("Vm.Pm.Business.Models.Company", "Company")
                        .WithMany("Phones")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Vm.Pm.Business.Models.Contact", "Contact")
                        .WithMany("Phones")
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}
