﻿// <auto-generated />
using System;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220221064504_Mssql")]
    partial class Mssql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EntityFramework.Models.Bank", b =>
                {
                    b.Property<int?>("BankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BankIdentifier")
                        .HasColumnType("longtext");

                    b.Property<string>("BankName")
                        .HasColumnType("longtext");

                    b.Property<string>("HeadQuartersAddress")
                        .HasColumnType("longtext");

                    b.HasKey("BankID");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("EntityFramework.Models.Loans", b =>
                {
                    b.Property<int?>("LoanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Expire")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("Issued")
                        .HasColumnType("datetime");

                    b.Property<int?>("PayBackBalance")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLoanOfMoney")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(95)");

                    b.HasKey("LoanID");

                    b.HasIndex("UserID");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("EntityFramework.Models.Location", b =>
                {
                    b.Property<int?>("SubLocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BankID")
                        .HasColumnType("int");

                    b.Property<string>("SubLocationName")
                        .HasColumnType("longtext");

                    b.HasKey("SubLocationID");

                    b.HasIndex("BankID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("EntityFramework.Models.Rki", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Expire")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("Issued")
                        .HasColumnType("datetime");

                    b.Property<int?>("LoanID")
                        .HasColumnType("int");

                    b.Property<int?>("PayBackBalance")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLoanOfMoney")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Rki");
                });

            modelBuilder.Entity("EntityFramework.Models.SubBankAccounts", b =>
                {
                    b.Property<int?>("SubBankAccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("SubAccountName")
                        .HasColumnType("longtext");

                    b.Property<int>("SubBankAccountType")
                        .HasColumnType("int");

                    b.Property<string>("UsersUserID")
                        .HasColumnType("varchar(95)");

                    b.HasKey("SubBankAccountID");

                    b.HasIndex("UsersUserID");

                    b.ToTable("SubBankAccounts");
                });

            modelBuilder.Entity("EntityFramework.Models.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("balance")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime");

                    b.Property<string>("money")
                        .HasColumnType("longtext");

                    b.Property<string>("reciver")
                        .HasColumnType("longtext");

                    b.Property<string>("userID")
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("userID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("EntityFramework.Models.Users", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(95)");

                    b.Property<int?>("Balance")
                        .HasColumnType("int");

                    b.Property<int?>("BankID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

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

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("longtext");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime");

                            b1.Property<string>("ReplaceByToken")
                                .HasColumnType("longtext");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("longtext");

                            b1.Property<string>("Token")
                                .HasColumnType("longtext");

                            b1.Property<string>("UsersUserID")
                                .IsRequired()
                                .HasColumnType("varchar(95)");

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