﻿// <auto-generated />
using System;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    [DbContext(typeof(ExpenseDbContext))]
    [Migration("20230624150252_update date field UserExpense fix1")]
    partial class updatedatefieldUserExpensefix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Models.UserApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserExpensesListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserExpensesListId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpenseGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MonthChosenForGoal")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserExpensesListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserExpensesListId");

                    b.ToTable("UserExpensesGoals");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpensesList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserApplicationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserApplicationId");

                    b.ToTable("ExpensesLists");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserExpenseGoalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserExpenseGoalId");

                    b.ToTable("UserGoals");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserExpensesListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserExpensesListId");

                    b.ToTable("UserIncomes");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpense", b =>
                {
                    b.HasOne("Domain.Entities.Models.UserExpensesList", "UserExpensesList")
                        .WithMany("Expenses")
                        .HasForeignKey("UserExpensesListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserExpensesList");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpenseGoal", b =>
                {
                    b.HasOne("Domain.Entities.Models.UserExpensesList", "UserExpensesList")
                        .WithMany("UserGoals")
                        .HasForeignKey("UserExpensesListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserExpensesList");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpensesList", b =>
                {
                    b.HasOne("Domain.Entities.Models.UserApplication", "UserApplication")
                        .WithMany("ExpensesLists")
                        .HasForeignKey("UserApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserApplication");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserGoal", b =>
                {
                    b.HasOne("Domain.Entities.Models.UserExpenseGoal", "UserExpenseGoal")
                        .WithMany("UserCategoryGoals")
                        .HasForeignKey("UserExpenseGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserExpenseGoal");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserIncome", b =>
                {
                    b.HasOne("Domain.Entities.Models.UserExpensesList", "UserExpensesList")
                        .WithMany("UserIncomes")
                        .HasForeignKey("UserExpensesListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserExpensesList");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserApplication", b =>
                {
                    b.Navigation("ExpensesLists");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpenseGoal", b =>
                {
                    b.Navigation("UserCategoryGoals");
                });

            modelBuilder.Entity("Domain.Entities.Models.UserExpensesList", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("UserGoals");

                    b.Navigation("UserIncomes");
                });
#pragma warning restore 612, 618
        }
    }
}
