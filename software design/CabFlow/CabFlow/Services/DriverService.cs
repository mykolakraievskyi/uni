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

        public async void AddDriverAsync(Driver driver)
        {
            context.Drivers.Add(driver);
            await context.SaveChangesAsync();
        }
    }
}
