﻿// <auto-generated />
using System;
using AstroBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AstroBackEnd.Migrations
{
    [DbContext(typeof(AstroDataContext))]
    partial class AstroDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AstroBackEnd.Models.BirthChart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("BirthCharts");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Catagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catagories");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Horoscope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorLuck")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Love")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Money")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("NumberLuck")
                        .HasColumnType("real");

                    b.Property<string>("Work")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Horoscopes");
                });

            modelBuilder.Entity("AstroBackEnd.Models.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ImgLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ImgLinks");
                });

            modelBuilder.Entity("AstroBackEnd.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GeneratDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HtmlContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeleveryPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryAdress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderUserId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double?>("TotalCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderUserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AstroBackEnd.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("AstroBackEnd.Models.PlanetHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<int>("PlanetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("PlanetId");

                    b.ToTable("PlanetHouses");
                });

            modelBuilder.Entity("AstroBackEnd.Models.PlanetZodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanetId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.HasIndex("ZodiacId");

                    b.ToTable("PlanetZodiacs");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatagoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<int?>("Inventory")
                        .HasColumnType("int");

                    b.Property<int?>("MasterProductId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatagoryId");

                    b.HasIndex("MasterProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ProductZodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ZodiacId");

                    b.ToTable("ProductZodiacs");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("ZodiacId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ZodiacId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoroscopeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HoroscopeId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AstroBackEnd.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Zodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descreiption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZodiacDayEnd")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacDayStart")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacMonthEnd")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacMonthStart")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Zodiacs");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ZodiacHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("ZodiacId");

                    b.ToTable("ZodiacHouses");
                });

            modelBuilder.Entity("HoroscopeZodiac", b =>
                {
                    b.Property<int>("HoroscopesId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacsId")
                        .HasColumnType("int");

                    b.HasKey("HoroscopesId", "ZodiacsId");

                    b.HasIndex("ZodiacsId");

                    b.ToTable("HoroscopeZodiac");
                });

            modelBuilder.Entity("ProductZodiac", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacsId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "ZodiacsId");

                    b.HasIndex("ZodiacsId");

                    b.ToTable("ProductZodiac");
                });

            modelBuilder.Entity("AstroBackEnd.Models.BirthChart", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Profile", "Profile")
                        .WithOne("BirthChart")
                        .HasForeignKey("AstroBackEnd.Models.BirthChart", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ImgLink", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Product", null)
                        .WithMany("ImgLinks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AstroBackEnd.Models.Order", b =>
                {
                    b.HasOne("AstroBackEnd.Models.User", "OrderUser")
                        .WithMany("Orders")
                        .HasForeignKey("OrderUserId");

                    b.Navigation("OrderUser");
                });

            modelBuilder.Entity("AstroBackEnd.Models.OrderDetail", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("AstroBackEnd.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AstroBackEnd.Models.PlanetHouse", b =>
                {
                    b.HasOne("AstroBackEnd.Models.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Planet", "Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("AstroBackEnd.Models.PlanetZodiac", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Planet", "Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Zodiac", "Zodiac")
                        .WithMany()
                        .HasForeignKey("ZodiacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planet");

                    b.Navigation("Zodiac");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Product", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Catagory", "Catagory")
                        .WithMany()
                        .HasForeignKey("CatagoryId");

                    b.HasOne("AstroBackEnd.Models.Product", "MasterProduct")
                        .WithMany("ProductVariation")
                        .HasForeignKey("MasterProductId");

                    b.Navigation("Catagory");

                    b.Navigation("MasterProduct");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ProductZodiac", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Zodiac", "Zodiac")
                        .WithMany()
                        .HasForeignKey("ZodiacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Zodiac");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Profile", b =>
                {
                    b.HasOne("AstroBackEnd.Models.User", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UserId");

                    b.HasOne("AstroBackEnd.Models.Zodiac", "Zodiac")
                        .WithMany()
                        .HasForeignKey("ZodiacId");

                    b.Navigation("Zodiac");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Quote", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Horoscope", null)
                        .WithMany("Quotes")
                        .HasForeignKey("HoroscopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AstroBackEnd.Models.User", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AstroBackEnd.Models.ZodiacHouse", b =>
                {
                    b.HasOne("AstroBackEnd.Models.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Zodiac", "Zodiac")
                        .WithMany()
                        .HasForeignKey("ZodiacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");

                    b.Navigation("Zodiac");
                });

            modelBuilder.Entity("HoroscopeZodiac", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Horoscope", null)
                        .WithMany()
                        .HasForeignKey("HoroscopesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Zodiac", null)
                        .WithMany()
                        .HasForeignKey("ZodiacsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductZodiac", b =>
                {
                    b.HasOne("AstroBackEnd.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstroBackEnd.Models.Zodiac", null)
                        .WithMany()
                        .HasForeignKey("ZodiacsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AstroBackEnd.Models.Horoscope", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Product", b =>
                {
                    b.Navigation("ImgLinks");

                    b.Navigation("ProductVariation");
                });

            modelBuilder.Entity("AstroBackEnd.Models.Profile", b =>
                {
                    b.Navigation("BirthChart");
                });

            modelBuilder.Entity("AstroBackEnd.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
