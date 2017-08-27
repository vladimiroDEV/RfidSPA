using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RfidSPA.Data;

namespace RfidSPA.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170826212056_TransactionChange_1")]
    partial class TransactionChange_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.Anagrafica", b =>
                {
                    b.Property<long>("AnagraficaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("Cognome");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<string>("Nome");

                    b.Property<long>("StoreID");

                    b.Property<string>("Telefono");

                    b.HasKey("AnagraficaID");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("StoreID");

                    b.ToTable("Anagrafica");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDevice", b =>
                {
                    b.Property<long>("RfidDeviceID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<long?>("AnagraficaID");

                    b.Property<string>("ApplicationUserID");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<double?>("Credit");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<DateTime?>("JoinedDate");

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<string>("RfidDeviceCode")
                        .IsRequired();

                    b.Property<long?>("StoreID");

                    b.HasKey("RfidDeviceID");

                    b.HasIndex("AnagraficaID");

                    b.HasIndex("StoreID");

                    b.ToTable("RfidDevice");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDeviceHistory", b =>
                {
                    b.Property<long>("RfidDeviceHistoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<DateTime?>("InsertDate");

                    b.Property<DateTime>("OperationDate");

                    b.Property<long>("RfidDeviceID");

                    b.Property<int>("TypeOperation");

                    b.HasKey("RfidDeviceHistoryID");

                    b.HasIndex("RfidDeviceID");

                    b.ToTable("RfidDeviceHistory");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDeviceTransaction", b =>
                {
                    b.Property<long>("RfidDeviceTransactionID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AnagraficaID");

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("Descrizione");

                    b.Property<double?>("Importo");

                    b.Property<bool>("PaydOff");

                    b.Property<DateTime?>("PaydOffDate");

                    b.Property<string>("RfidDeviceCode");

                    b.Property<string>("RfidDeviceID");

                    b.Property<long?>("RfidDeviceID1");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactionOperation");

                    b.HasKey("RfidDeviceTransactionID");

                    b.HasIndex("AnagraficaID");

                    b.HasIndex("RfidDeviceID1");

                    b.ToTable("RfidDeviceTransaction");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.Store", b =>
                {
                    b.Property<long>("StoreID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Address");

                    b.Property<string>("AdministratorID");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<string>("CreatorUser");

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Telefono");

                    b.HasKey("StoreID");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.StoreUser", b =>
                {
                    b.Property<long>("StoreUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<long>("StoreID");

                    b.Property<string>("UserRole");

                    b.HasKey("StoreUserID");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("StoreID");

                    b.ToTable("StoreUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RfidSPA.Models.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.Anagrafica", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("RfidSPA.Models.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDevice", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.Anagrafica", "Anagrafica")
                        .WithMany()
                        .HasForeignKey("AnagraficaID");

                    b.HasOne("RfidSPA.Models.Entities.Store", "Store")
                        .WithMany("Devices")
                        .HasForeignKey("StoreID");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDeviceHistory", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.RfidDevice", "RfidDevice")
                        .WithMany()
                        .HasForeignKey("RfidDeviceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.RfidDeviceTransaction", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.Anagrafica", "Anagrafica")
                        .WithMany()
                        .HasForeignKey("AnagraficaID");

                    b.HasOne("RfidSPA.Models.Entities.RfidDevice")
                        .WithMany("RfidDeviceTransaction")
                        .HasForeignKey("RfidDeviceID1");
                });

            modelBuilder.Entity("RfidSPA.Models.Entities.StoreUser", b =>
                {
                    b.HasOne("RfidSPA.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("RfidSPA.Models.Entities.Store", "Store")
                        .WithMany("storeUsers")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
