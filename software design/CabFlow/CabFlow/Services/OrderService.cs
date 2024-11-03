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
    public class OrderService
    {
        private readonly CabFlowContext _context;
        public OrderService(CabFlowContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersAsync()
        {
            List<Order> orders;
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                orders = await _context.Orders.ToListAsync();
                scope.Complete();
            }

            return orders;
        }
        public Task<Order> GetOrderByIdAsync(int id)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async void AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async void UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async void DeleteOrderAsync(Order order)
        { 
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}
