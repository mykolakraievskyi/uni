using CabFlow.Data.CabFlowDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;

namespace CabFlow.Services
{
    public class VehicleService(ICabFlowContext context)
    {
        public Task<List<Vehicle>> GetVehiclesAsync()
        {
            return context.Vehicles.ToListAsync();
        }
        public Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }
        public async void AddVehicleAsync(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
        }

        public async void UpdateVehicleAsync(Vehicle vehicle)
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();
        }

        public async void DeleteVehicleAsync(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
        }
    }
}
