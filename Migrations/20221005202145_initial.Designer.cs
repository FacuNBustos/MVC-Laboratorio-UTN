// <auto-generated />
using System;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Biblioteca.Migrations
{
    [DbContext(typeof(appDBcontext))]
    [Migration("20221005202145_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25");

            modelBuilder.Entity("Biblioteca.Models.Author", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("biography")
                        .HasColumnType("TEXT");

                    b.Property<string>("fullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("photo")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("Biblioteca.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("authorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("genderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("photo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("publicationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("resume")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("authorID");

                    b.HasIndex("genderID");

                    b.ToTable("books");
                });

            modelBuilder.Entity("Biblioteca.Models.Gender", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("genderes");
                });

            modelBuilder.Entity("Biblioteca.Models.Book", b =>
                {
                    b.HasOne("Biblioteca.Models.Author", "author")
                        .WithMany()
                        .HasForeignKey("authorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteca.Models.Gender", "gender")
                        .WithMany()
                        .HasForeignKey("genderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
