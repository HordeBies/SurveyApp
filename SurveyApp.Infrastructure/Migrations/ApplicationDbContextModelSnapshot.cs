﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyApp.Infrastructure.DatabaseContexts;

#nullable disable

namespace SurveyApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SurveyApp.Domain.Entities.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Position")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Choice", b =>
                {
                    b.HasOne("SurveyApp.Domain.Entities.Question", null)
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Question", b =>
                {
                    b.HasOne("SurveyApp.Domain.Entities.Survey", null)
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Question", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("SurveyApp.Domain.Entities.Survey", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}