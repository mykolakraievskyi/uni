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
        public async void AddVehicleAsync(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
        }
    }
}
