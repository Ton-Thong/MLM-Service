﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MlmService.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MlmService.Migrations.Tenant
{
    [DbContext(typeof(TenantContext))]
    partial class TenantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MlmService.Database.Models.Core.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Amphure")
                        .HasColumnType("text");

                    b.Property<int?>("AmphureId")
                        .HasColumnType("integer");

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("District")
                        .HasColumnType("text");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Facebook")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Line")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("MembershipId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Prefix")
                        .HasColumnType("integer");

                    b.Property<string>("Province")
                        .HasColumnType("text");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("integer");

                    b.Property<string>("SponsorCode")
                        .HasColumnType("text");

                    b.Property<Guid?>("SponsorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Zipcode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.HasIndex("SponsorId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MlmService.Database.Models.Core.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Pv")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("MlmService.Database.Models.Core.Member", b =>
                {
                    b.HasOne("MlmService.Database.Models.Core.Package", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId");

                    b.HasOne("MlmService.Database.Models.Core.Member", "Sponsor")
                        .WithMany()
                        .HasForeignKey("SponsorId");

                    b.Navigation("Membership");

                    b.Navigation("Sponsor");
                });
#pragma warning restore 612, 618
        }
    }
}