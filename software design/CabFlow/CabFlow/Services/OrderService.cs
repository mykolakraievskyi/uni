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
    public class OrderService(ICabFlowContext context)
    {
        public Task<List<Order>> GetOrdersAsync()
        {
            return context.Orders.ToListAsync();
        }
        public Task<Order> GetOrderByIdAsync(int id)
        {
            return context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async void AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async void UpdateOrderAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async void DeleteOrderAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

    }
}
