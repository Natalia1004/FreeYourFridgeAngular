﻿// <auto-generated />
using System;
using FreeYourFridge.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FreeYourFridge.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201013164706_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("FreeYourFridge.API.Models.DailyMeal", b =>
                {
                    b.Property<Guid>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CaloriesPerPortion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Carbs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Grams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("Protein")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeOfLastMeal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserRemarks")
                        .HasColumnType("TEXT");

                    b.HasKey("LocalId");

                    b.ToTable("DailyMeals");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.DailyMealToArchive", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Calories")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Carbs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeAddDeailyMeal")
                        .HasColumnType("TEXT");

                    b.Property<int>("Fat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Grams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LocalId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Protein")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeOfLastMeal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserRemarks")
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("ArchivedDailyMeals");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Favoured", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpoonacularId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Favoureds");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Fridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Fridges");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int?>("FridgeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpoonacularId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FridgeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.ListOfIngredients", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("originalName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("ListOfIngredients");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Meal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Grams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpoonacularId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.ShoppingListItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOnShoppingList")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpoonacularId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Height")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.UserDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActivityLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Carbohydrates")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DailyDemand")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DailyDemandToRealize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Fats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Protein")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsersDetails");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Fridge", b =>
                {
                    b.HasOne("FreeYourFridge.API.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.Ingredient", b =>
                {
                    b.HasOne("FreeYourFridge.API.Models.Fridge", "Fridge")
                        .WithMany("ListIgredients")
                        .HasForeignKey("FridgeId");
                });

            modelBuilder.Entity("FreeYourFridge.API.Models.UserDetails", b =>
                {
                    b.HasOne("FreeYourFridge.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}