using System;
using IssueTracker.Common;
using IssueTracker.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Persistance
{
    public class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<IssueEntity> Issues { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<IssueTransitionEntity> Type { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueTransitionEntity>()
                .HasOne(i => i.MadeBy)
                .WithMany(u => u.IssueTransitions)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueEntity>()
                .HasOne(i => i.CreatedBy)
                .WithMany(u => u.Issues)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>().Property(u => u.IsDeleted).HasDefaultValue(false);

        }


    }
}
