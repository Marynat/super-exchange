﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using super_exchange.Server.Database;

#nullable disable

namespace super_exchange.Server.Migrations
{
    [DbContext(typeof(ExchangeDatabaseContext))]
    [Migration("20240916140240_UpdateColumnsToMoney")]
    partial class UpdateColumnsToMoney
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("super_exchange.Server.Entity.RateEntity", b =>
                {
                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Ask")
                        .HasColumnType("money");

                    b.Property<decimal>("Bid")
                        .HasColumnType("money");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Mid")
                        .HasColumnType("money");

                    b.Property<int>("Symbol")
                        .HasColumnType("int");

                    b.Property<DateTime>("TradingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EffectiveDate", "Code");

                    b.ToTable("RateEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
