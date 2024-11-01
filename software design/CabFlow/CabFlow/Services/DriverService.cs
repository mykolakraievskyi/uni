using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Data.CabFlowDbContext;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;

namespace CabFlow.Services
{
    public class DriverService(ICabFlowContext context)
    {
        public Task<List<Driver>> GetDriversAsync()
        {
            return context.Drivers.ToListAsync();
        }

        public Task<Driver> GetDriverByIdAsync(int id)
        {
            return context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async void AddDriverAsync(Driver driver)
        {
            context.Drivers.Add(driver);
            await context.SaveChangesAsync();
        }

        public async void UpdateDriverAsync(Driver driver)
        {
            context.Drivers.Update(driver);
            await context.SaveChangesAsync();
        }
        
        public async void DeleteDriverAsync(Driver driver)
        {
            context.Drivers.Remove(driver);
            await context.SaveChangesAsync();
        }
    }
}
