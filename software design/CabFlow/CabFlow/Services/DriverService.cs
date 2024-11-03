using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media.Imaging;
using CabFlow.Data.CabFlowDbContext;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;

namespace CabFlow.Services
{
    public class DriverService
    {
        private readonly CabFlowContext _context;
        public DriverService(CabFlowContext context)
        {
            _context = context;
        }
        public async Task<List<Driver>> GetDriversAsync()
        {
            List<Driver> drivers;
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                drivers = await _context.Drivers.ToListAsync();
                scope.Complete();
            }

            return drivers;
        }

        public Task<Driver> GetDriverByIdAsync(int id)
        {
            return _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async void AddDriverAsync(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
        }

        public async void UpdateDriverAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }
        
        public async void DeleteDriverAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }
    }
}
