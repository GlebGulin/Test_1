using Microsoft.EntityFrameworkCore;
using Models.SingleOrder;
using System;

namespace DAL
{
    public class Db : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<Dropoff> dropoffs { get; set; }
        public DbSet<Pickup> pickups { get; set; }
        public Db(DbContextOptions<Db> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dropoff>().HasData(
                new Dropoff
                {
                    Id = 1,
                    latitude = 0,
                    longitude = 0
                },
                new Dropoff
                {
                    Id = 2,
                    latitude = 1,
                    longitude = 1
                },
                new Dropoff
                {
                    Id = 3,
                    latitude = 3,
                    longitude = 3
                }

            );
            modelBuilder.Entity<Pickup>().HasData(
                new Pickup
                {
                    Id = 1,
                    latitude = 0,
                    longitude = 0
                },
                new Pickup
                {
                    Id = 2,
                    latitude = 1,
                    longitude = 1
                },
                 new Pickup
                 {
                     Id = 3,
                     latitude = 4,
                     longitude = 7
                 }

            );
            modelBuilder.Entity<Order>().HasData(
               new Order
               {
                   Id = 1,
                   dimension = "dimension",
                   status = "Status1",
                   PickupId = 1,
                   DropoffId = 1
               },
               new Order
               {
                   Id = 2,
                   dimension = "any",
                   status = "Status1",
                   PickupId = 2,
                   DropoffId = 2
               },
                new Order
                {
                    Id = 3,
                    dimension = "any",
                    status = "Status2",
                    PickupId = 2,
                    DropoffId = 2
                }



           );
        }

        }

    }
