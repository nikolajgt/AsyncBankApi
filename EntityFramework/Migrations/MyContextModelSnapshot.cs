// <auto-generated />
using System;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EntityFramework.Models.Bank", b =>
                {
                    b.Property<int?>("BankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("BankID"), 1L, 1);

                    b.Property<string>("BankIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadQuartersAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BankID");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("EntityFramework.Models.Loans", b =>
                {
                    b.Property<int?>("LoanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("LoanID"), 1L, 1);

                    b.Property<DateTime?>("Expire")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Issued")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PayBackBalance")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLoanOfMoney")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoanID");

                    b.HasIndex("UserID");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("EntityFramework.Models.Location", b =>
                {
                    b.Property<int?>("SubLocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("SubLocationID"), 1L, 1);

                    b.Property<int?>("BankID")
                        .HasColumnType("int");

                    b.Property<string>("SubLocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubLocationID");

                    b.HasIndex("BankID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("EntityFramework.Models.Rki", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<DateTime?>("Expire")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Issued")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LoanID")
                        .HasColumnType("int");

                    b.Property<int?>("PayBackBalance")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLoanOfMoney")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Rki");
                });

            modelBuilder.Entity("EntityFramework.Models.SubBankAccounts", b =>
                {
                    b.Property<int?>("SubBankAccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("SubBankAccountID"), 1L, 1);

                    b.Property<int?>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("SubAccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubBankAccountType")
                        .HasColumnType("int");

                    b.Property<string>("UsersUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubBankAccountID");

                    b.HasIndex("UsersUserID");

                    b.ToTable("SubBankAccounts");
                });

            modelBuilder.Entity("EntityFramework.Models.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("balance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("money")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reciver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("userID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("EntityFramework.Models.Users", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Balance")
                        .HasColumnType("int");

                    b.Property<int?>("BankID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("BankID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EntityFramework.Models.Loans", b =>
                {
                    b.HasOne("EntityFramework.Models.Users", "Userid")
                        .WithMany("Loans")
                        .HasForeignKey("UserID");

                    b.Navigation("Userid");
                });

            modelBuilder.Entity("EntityFramework.Models.Location", b =>
                {
                    b.HasOne("EntityFramework.Models.Bank", null)
                        .WithMany("SubLocations")
                        .HasForeignKey("BankID");
                });

            modelBuilder.Entity("EntityFramework.Models.Rki", b =>
                {
                    b.HasOne("EntityFramework.Models.Users", "Userid")
                        .WithMany("Rki")
                        .HasForeignKey("UserID");

                    b.Navigation("Userid");
                });

            modelBuilder.Entity("EntityFramework.Models.SubBankAccounts", b =>
                {
                    b.HasOne("EntityFramework.Models.Users", null)
                        .WithMany("SubBankAccounts")
                        .HasForeignKey("UsersUserID");
                });

            modelBuilder.Entity("EntityFramework.Models.Transactions", b =>
                {
                    b.HasOne("EntityFramework.Models.Users", "user")
                        .WithMany("Transactions")
                        .HasForeignKey("userID");

                    b.Navigation("user");
                });

            modelBuilder.Entity("EntityFramework.Models.Users", b =>
                {
                    b.HasOne("EntityFramework.Models.Bank", "Bank")
                        .WithMany("Users")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("EntityFramework.Models.JWT.RefreshToken", "RefreshToken", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ReplaceByToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UsersUserID")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("Id");

                            b1.HasIndex("UsersUserID");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("UsersUserID");
                        });

                    b.Navigation("Bank");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("EntityFramework.Models.Bank", b =>
                {
                    b.Navigation("SubLocations");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EntityFramework.Models.Users", b =>
                {
                    b.Navigation("Loans");

                    b.Navigation("Rki");

                    b.Navigation("SubBankAccounts");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
