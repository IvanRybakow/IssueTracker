﻿// <auto-generated />
using System;
using IssueTracker.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IssueTracker.Persistance.Migrations
{
    [DbContext(typeof(IssueTrackerContext))]
    [Migration("20180730122010_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IssueTracker.Common.Models.IssueEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<int>("Priority");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("Status");

                    b.Property<int>("Urgency");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("IssueTracker.Common.Models.IssueTransitionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Command");

                    b.Property<string>("Comment");

                    b.Property<int>("IssueId");

                    b.Property<int>("MadeById");

                    b.Property<DateTime>("TransitionDate");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("MadeById");

                    b.ToTable("IssueTransitions");
                });

            modelBuilder.Entity("IssueTracker.Common.Models.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Login");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IssueTracker.Common.Models.IssueEntity", b =>
                {
                    b.HasOne("IssueTracker.Common.Models.UserEntity", "CreatedBy")
                        .WithMany("Issues")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("IssueTracker.Common.Models.IssueTransitionEntity", b =>
                {
                    b.HasOne("IssueTracker.Common.Models.IssueEntity", "Issue")
                        .WithMany("Transitions")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IssueTracker.Common.Models.UserEntity", "MadeBy")
                        .WithMany("IssueTransitions")
                        .HasForeignKey("MadeById")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
