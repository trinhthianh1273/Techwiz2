﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoccerManager.Models;

#nullable disable

namespace SoccerManager.Migrations
{
    [DbContext(typeof(SoccerContext))]
    [Migration("20230811023552_sua-img-url-length")]
    partial class suaimgurllength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SoccerManager.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("AddressID");

                    b.HasIndex("CustomerID");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("SoccerManager.Models.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("ProductID")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ProductID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("SoccerManager.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SoccerManager.Models.Competition", b =>
                {
                    b.Property<int>("CompetitionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CompetitionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetitionID"));

                    b.Property<string>("CompetitionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CompetitionID");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("SoccerManager.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Fullname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CustomerID");

                    b.HasIndex(new[] { "Username" }, "UQ__Customer__536C85E4282B50A6")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("SoccerManager.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FeedbackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"));

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FeedbackID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("SoccerManager.Models.Match", b =>
                {
                    b.Property<int>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MatchID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchID"));

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int")
                        .HasColumnName("CompetitionID");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<int>("GuestTeamID")
                        .HasColumnType("int")
                        .HasColumnName("GuestTeamID");

                    b.Property<int?>("GuestTeamScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamID")
                        .HasColumnType("int")
                        .HasColumnName("HomeTeamID");

                    b.Property<int?>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<string>("MatchName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Stadium")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime");

                    b.HasKey("MatchID");

                    b.HasIndex("CompetitionID");

                    b.HasIndex("GuestTeamID");

                    b.HasIndex("HomeTeamID");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("SoccerManager.Models.OrderContent", b =>
                {
                    b.Property<int>("OrderContentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderContentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderContentID"));

                    b.Property<int>("OrderID")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductID")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderContentID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderContent");
                });

            modelBuilder.Entity("SoccerManager.Models.Orders", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("AddressID")
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    b.Property<string>("CardName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CardNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<string>("Expire")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("PaymentMethodID")
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    b.Property<int?>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("SecurityCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("StatusID")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("OrderID")
                        .HasName("PK__Orders__C3905BAFD40F2FBD");

                    b.HasIndex("AddressID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("StatusID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodID"));

                    b.Property<string>("PaymentMethod1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PaymentMethod");

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("SoccerManager.Models.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"));

                    b.Property<int?>("CurrentTeam")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Pob")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerID");

                    b.HasIndex("CurrentTeam");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SoccerManager.Models.PlayerImage", b =>
                {
                    b.Property<int>("PlayerImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerImageID"));

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.HasKey("PlayerImageID");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayerImage");
                });

            modelBuilder.Entity("SoccerManager.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductImageID"));

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("ProductID")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("ProductImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("SoccerManager.Models.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Discontinued")
                        .HasColumnType("int");

                    b.Property<int?>("InStock")
                        .HasColumnType("int");

                    b.Property<int?>("OnOrder")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TeamID")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SoccerManager.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SoccerManager.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamID"));

                    b.Property<string>("FoundedPosition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FoundedYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manager")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Owner")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TeamID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamImage", b =>
                {
                    b.Property<int>("TeamImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeamImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamImageID"));

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("TeamID")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("TeamImageID");

                    b.HasIndex("TeamID");

                    b.ToTable("TeamImage");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamPlayerHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("LeaveDate")
                        .HasColumnType("date");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.Property<int>("TeamID")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("Id");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("TeamPlayerHistory");
                });

            modelBuilder.Entity("SoccerManager.Models.Address", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Address")
                        .HasForeignKey("CustomerID")
                        .IsRequired()
                        .HasConstraintName("FK_address_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Cart", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Cart")
                        .HasForeignKey("CustomerID")
                        .IsRequired()
                        .HasConstraintName("FK_Cart_Customer");

                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("Cart")
                        .HasForeignKey("ProductID")
                        .IsRequired()
                        .HasConstraintName("FK_CartContent_Products");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Feedback", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Feedback")
                        .HasForeignKey("CustomerID")
                        .IsRequired()
                        .HasConstraintName("FK_Feedback_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Match", b =>
                {
                    b.HasOne("SoccerManager.Models.Competition", "Competition")
                        .WithMany("Match")
                        .HasForeignKey("CompetitionID")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Competition");

                    b.HasOne("SoccerManager.Models.Team", "GuestTeam")
                        .WithMany("MatchGuestTeam")
                        .HasForeignKey("GuestTeamID")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Team1");

                    b.HasOne("SoccerManager.Models.Team", "HomeTeam")
                        .WithMany("MatchHomeTeam")
                        .HasForeignKey("HomeTeamID")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Team");

                    b.Navigation("Competition");

                    b.Navigation("GuestTeam");

                    b.Navigation("HomeTeam");
                });

            modelBuilder.Entity("SoccerManager.Models.OrderContent", b =>
                {
                    b.HasOne("SoccerManager.Models.Orders", "Order")
                        .WithMany("OrderContent")
                        .HasForeignKey("OrderID")
                        .IsRequired()
                        .HasConstraintName("FK_OrderContent_Orders");

                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("OrderContent")
                        .HasForeignKey("ProductID")
                        .IsRequired()
                        .HasConstraintName("FK_OrderContent_Products");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Orders", b =>
                {
                    b.HasOne("SoccerManager.Models.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressID")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_address");

                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Customer");

                    b.HasOne("SoccerManager.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeID")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Employee");

                    b.HasOne("SoccerManager.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodID")
                        .HasConstraintName("FK_Orders_PaymentMethod");

                    b.HasOne("SoccerManager.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusID")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Status");

                    b.Navigation("Address");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SoccerManager.Models.Player", b =>
                {
                    b.HasOne("SoccerManager.Models.Team", "CurrentTeamNavigation")
                        .WithMany("Player")
                        .HasForeignKey("CurrentTeam")
                        .HasConstraintName("FK_Player_Team");

                    b.Navigation("CurrentTeamNavigation");
                });

            modelBuilder.Entity("SoccerManager.Models.PlayerImage", b =>
                {
                    b.HasOne("SoccerManager.Models.Player", "Player")
                        .WithMany("PlayerImage")
                        .HasForeignKey("PlayerID")
                        .IsRequired()
                        .HasConstraintName("FK_PlayerImage_Player");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SoccerManager.Models.ProductImage", b =>
                {
                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductID")
                        .IsRequired()
                        .HasConstraintName("FK_ProductImage_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Products", b =>
                {
                    b.HasOne("SoccerManager.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .HasConstraintName("FK_Products_Category");

                    b.HasOne("SoccerManager.Models.Player", "Player")
                        .WithMany("Products")
                        .HasForeignKey("PlayerID")
                        .HasConstraintName("FK_Products_Player");

                    b.HasOne("SoccerManager.Models.Team", "Team")
                        .WithMany("Products")
                        .HasForeignKey("TeamID")
                        .IsRequired()
                        .HasConstraintName("FK_Products_Team");

                    b.Navigation("Category");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamImage", b =>
                {
                    b.HasOne("SoccerManager.Models.Team", "Team")
                        .WithMany("TeamImage")
                        .HasForeignKey("TeamID")
                        .IsRequired()
                        .HasConstraintName("FK_TeamImage_Team");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamPlayerHistory", b =>
                {
                    b.HasOne("SoccerManager.Models.Player", "Player")
                        .WithMany("TeamPlayerHistory")
                        .HasForeignKey("PlayerID")
                        .IsRequired()
                        .HasConstraintName("FK_TeamPlayerHistory_Player");

                    b.HasOne("SoccerManager.Models.Team", "Team")
                        .WithMany("TeamPlayerHistory")
                        .HasForeignKey("TeamID")
                        .IsRequired()
                        .HasConstraintName("FK_TeamPlayerHistory_Team");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SoccerManager.Models.Competition", b =>
                {
                    b.Navigation("Match");
                });

            modelBuilder.Entity("SoccerManager.Models.Customer", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Cart");

                    b.Navigation("Feedback");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.Orders", b =>
                {
                    b.Navigation("OrderContent");
                });

            modelBuilder.Entity("SoccerManager.Models.PaymentMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.Player", b =>
                {
                    b.Navigation("PlayerImage");

                    b.Navigation("Products");

                    b.Navigation("TeamPlayerHistory");
                });

            modelBuilder.Entity("SoccerManager.Models.Products", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("OrderContent");

                    b.Navigation("ProductImage");
                });

            modelBuilder.Entity("SoccerManager.Models.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.Team", b =>
                {
                    b.Navigation("MatchGuestTeam");

                    b.Navigation("MatchHomeTeam");

                    b.Navigation("Player");

                    b.Navigation("Products");

                    b.Navigation("TeamImage");

                    b.Navigation("TeamPlayerHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
