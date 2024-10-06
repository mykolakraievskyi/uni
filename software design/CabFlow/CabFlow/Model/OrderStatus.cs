using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Model
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region EF Navigation
        public List<Order> Orders { get; set; }

        #endregion
    }
}
