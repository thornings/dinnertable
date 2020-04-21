﻿// <auto-generated />
using System;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientMadbordet.Migrations
{
    [DbContext(typeof(CalendarContext))]
    [Migration("20200416060832_AddWeigtTyp")]
    partial class AddWeigtTyp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClientMadbordet.Models.CalendarFoodItem", b =>
                {
                    b.Property<int>("CalendarFoodItemID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CalendarDate");

                    b.Property<int?>("FoodID");

                    b.Property<int?>("MealID");

                    b.Property<int>("Weight");

                    b.HasKey("CalendarFoodItemID");

                    b.HasIndex("FoodID");

                    b.HasIndex("MealID");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("ClientMadbordet.Models.Food", b =>
                {
                    b.Property<int>("FoodID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Carb")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Energy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Fat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Protein")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FoodID");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("ClientMadbordet.Models.FoodWeightType", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("WeightTypeId");

                    b.Property<int>("Weight");

                    b.HasKey("FoodId", "WeightTypeId");

                    b.HasIndex("WeightTypeId");

                    b.ToTable("FoodWeightTypes");
                });

            modelBuilder.Entity("ClientMadbordet.Models.Meal", b =>
                {
                    b.Property<int>("MealID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("MealID");

                    b.HasAlternateKey("Name");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("ClientMadbordet.Models.WeightType", b =>
                {
                    b.Property<int>("WTID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("WTID");

                    b.ToTable("WeightTypes");
                });

            modelBuilder.Entity("ClientMadbordet.Models.CalendarFoodItem", b =>
                {
                    b.HasOne("ClientMadbordet.Models.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodID");

                    b.HasOne("ClientMadbordet.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealID");
                });

            modelBuilder.Entity("ClientMadbordet.Models.FoodWeightType", b =>
                {
                    b.HasOne("ClientMadbordet.Models.Food", "Food")
                        .WithMany("FoodWeightTypes")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClientMadbordet.Models.WeightType", "WeightType")
                        .WithMany("FoodWeightTypes")
                        .HasForeignKey("WeightTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
