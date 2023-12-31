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
    [Migration("20230810063415_add-logo-2")]
    partial class addlogo2
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
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address");

                    b.Property<int>("CustomerId")
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

                    b.HasKey("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("SoccerManager.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("SoccerManager.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
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

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SoccerManager.Models.Competition", b =>
                {
                    b.Property<int>("CompetitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CompetitionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetitionId"));

                    b.Property<string>("CompetitionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CompetitionId");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("SoccerManager.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

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

                    b.HasKey("CustomerId");

                    b.HasIndex(new[] { "Username" }, "UQ__Customer__536C85E4282B50A6")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

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

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("SoccerManager.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FeedbackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("SoccerManager.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MatchID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchId"));

                    b.Property<int>("CompetitionId")
                        .HasColumnType("int")
                        .HasColumnName("CompetitionID");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<int>("GuestTeamId")
                        .HasColumnType("int")
                        .HasColumnName("GuestTeamID");

                    b.Property<int?>("GuestTeamScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
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

                    b.HasKey("MatchId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("GuestTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("SoccerManager.Models.OrderContent", b =>
                {
                    b.Property<int>("OrderContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderContentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderContentId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderContentId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderContent");
                });

            modelBuilder.Entity("SoccerManager.Models.Orders", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    b.Property<string>("CardName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CardNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<string>("Expire")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    b.Property<int?>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("SecurityCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BAFD40F2FBD");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SoccerManager.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<string>("PaymentMethod1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PaymentMethod");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("SoccerManager.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"));

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

                    b.HasKey("PlayerId");

                    b.HasIndex("CurrentTeam");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SoccerManager.Models.PlayerImage", b =>
                {
                    b.Property<int>("PlayerImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerImageId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.HasKey("PlayerImageId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerImage");
                });

            modelBuilder.Entity("SoccerManager.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductImageId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("ProductImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("SoccerManager.Models.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int?>("CategoryId")
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

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SoccerManager.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SoccerManager.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

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

                    b.Property<string>("LogoUrl")
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

                    b.HasKey("TeamId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamImage", b =>
                {
                    b.Property<int>("TeamImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeamImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamImageId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ImageURL");

                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("TeamImageId");

                    b.HasIndex("TeamId");

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

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayerHistory");
                });

            modelBuilder.Entity("SoccerManager.Models.Address", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Address")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_address_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Cart", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Cart")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Cart_Customer");

                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("Cart")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_CartContent_Products");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Feedback", b =>
                {
                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Feedback")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Feedback_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SoccerManager.Models.Match", b =>
                {
                    b.HasOne("SoccerManager.Models.Competition", "Competition")
                        .WithMany("Match")
                        .HasForeignKey("CompetitionId")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Competition");

                    b.HasOne("SoccerManager.Models.Team", "GuestTeam")
                        .WithMany("MatchGuestTeam")
                        .HasForeignKey("GuestTeamId")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Team1");

                    b.HasOne("SoccerManager.Models.Team", "HomeTeam")
                        .WithMany("MatchHomeTeam")
                        .HasForeignKey("HomeTeamId")
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
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderContent_Orders");

                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("OrderContent")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderContent_Products");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Orders", b =>
                {
                    b.HasOne("SoccerManager.Models.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_address");

                    b.HasOne("SoccerManager.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Customer");

                    b.HasOne("SoccerManager.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Employee");

                    b.HasOne("SoccerManager.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodId")
                        .HasConstraintName("FK_Orders_PaymentMethod");

                    b.HasOne("SoccerManager.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
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
                        .HasForeignKey("PlayerId")
                        .IsRequired()
                        .HasConstraintName("FK_PlayerImage_Player");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SoccerManager.Models.ProductImage", b =>
                {
                    b.HasOne("SoccerManager.Models.Products", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductImage_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SoccerManager.Models.Products", b =>
                {
                    b.HasOne("SoccerManager.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Products_Category");

                    b.HasOne("SoccerManager.Models.Player", "Player")
                        .WithMany("Products")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("FK_Products_Player");

                    b.HasOne("SoccerManager.Models.Team", "Team")
                        .WithMany("Products")
                        .HasForeignKey("TeamId")
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
                        .HasForeignKey("TeamId")
                        .IsRequired()
                        .HasConstraintName("FK_TeamImage_Team");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Models.TeamPlayerHistory", b =>
                {
                    b.HasOne("SoccerManager.Models.Player", "Player")
                        .WithMany("TeamPlayerHistory")
                        .HasForeignKey("PlayerId")
                        .IsRequired()
                        .HasConstraintName("FK_TeamPlayerHistory_Player");

                    b.HasOne("SoccerManager.Models.Team", "Team")
                        .WithMany("TeamPlayerHistory")
                        .HasForeignKey("TeamId")
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
