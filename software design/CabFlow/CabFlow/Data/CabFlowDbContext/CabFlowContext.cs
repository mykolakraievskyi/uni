using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Data.Configuration;
using CabFlow.Data.Configurtion;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CabFlow.Data.CabFlowDbContext
{
    public class CabFlowContext(DbContextOptions options) : DbContext(options), ICabFlowContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DriverConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new OrderStatusConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new PointConfig());

            modelBuilder.Entity<Category>().HasData(
                new Category() {Name = "A"},
                new Category() {Name = "B"},
                new Category() {Name = "C"},
                new Category() {Name = "D"},
                new Category() {Name = "E"}
            );
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus(){ Name = "Planned"},
                new OrderStatus(){ Name = "In progress"},
                new OrderStatus(){ Name = "Done"},
                new OrderStatus(){ Name = "Cancelled"}
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
