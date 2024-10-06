using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime PickUpTime { get; set; }

        #region EF Navigation 
        public List<Point> Points { get; set; }
        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public OrderStatus OrderStatus { get; set; }

        #endregion
    }
}
