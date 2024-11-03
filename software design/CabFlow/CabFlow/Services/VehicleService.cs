using CabFlow.Data.CabFlowDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;

namespace CabFlow.Services
{
    public class VehicleService
    {
        private readonly CabFlowContext _context;
        public VehicleService(CabFlowContext context)
        {
            _context = context;
        }
        public List<Vehicle> GetVehiclesAsync()
        {
            List<Vehicle> vehicles;
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                vehicles =  _context.Vehicles.ToList();
                scope.Complete();
            }

            return vehicles;
        }
        public Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }
        public async void AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async void UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async void DeleteVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
