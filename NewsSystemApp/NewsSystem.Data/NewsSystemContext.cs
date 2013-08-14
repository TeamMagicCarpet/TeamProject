﻿using NewsSystem.Models;
using System.Data.Entity;

namespace NewsSystem.Data
{
    public class NewsSystemContext : DbContext
    {
        public NewsSystemContext()
            : base(@"NewsSystemDb")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Image> Images { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(40);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.SessionKey)
                .IsOptional()
                .IsFixedLength()
                .HasMaxLength(50);

            modelBuilder.Entity<Article>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Article>()
                .Property(a => a.CreationDate)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .HasRequired(a => a.Author);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Article);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User);

            modelBuilder.Entity<Image>()
                .HasRequired(i => i.Article);

            modelBuilder.Entity<Image>()
                .Property(i => i.Location)
                .IsRequired();

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.Article);

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.User);

            modelBuilder.Entity<Vote>()
                .Property(v => v.Value)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
