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
    public class CabFlowContext: DbContext
    {
        public CabFlowContext(DbContextOptions<CabFlowContext> options) : base(options)
        {
        }

        public CabFlowContext() {}
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // check if configuration is already done
            if (!optionsBuilder.IsConfigured)
            {
                // resolve connection string from appsettings.json
                var connectionString = ConfigurationManager.ConnectionStrings["CabFlow"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }

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
                new Category() { Id = 1, Name = "A" },
                new Category() { Id = 2, Name = "B" },
                new Category() { Id = 3, Name = "C" },
                new Category() { Id = 4, Name = "D" },
                new Category() { Id = 5, Name = "E" }
            );
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus() { Id = 1, Name = "Planned" },
                new OrderStatus() { Id = 2, Name = "In progress" },
                new OrderStatus() { Id = 3, Name = "Done" },
                new OrderStatus() { Id = 4, Name = "Cancelled" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
