﻿using NewsSystem.Models;
using System.Data.Entity;

namespace NewsSystem.Data
{
    public class NewsSystemContext : DbContext
    {
        public NewsSystemContext()
            : base(@"
            Data Source=05bbefd6-d391-4b3d-bb0b-a21a00e7307f.sqlserver.sequelizer.com;
            Initial Catalog=db05bbefd6d3914b3dbb0ba21a00e7307f;
            User ID=hryjgtvdzooafwel;
            Password=DVni8wBJvUQ4iahYm26nRNNV5ussqrEoGhQsjbA4mvbAe586ASVu56RYgzmMdjte;
            providerName=System.Data.EntityClient;
            multipleactiveresultsets=True;")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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

            modelBuilder.Entity<Article>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
