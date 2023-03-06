﻿using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Seed;
using System;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a=>a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
              .Property(a => a.Street)
              .IsRequired()
              .HasMaxLength(50);

            modelBuilder.Seed();
        }
    }
}
