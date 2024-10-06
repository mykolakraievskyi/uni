using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CabFlow.Model
{
    public class Point
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        #region EF Navigation
        public List<Order> Orders { get; set; }

        #endregion

    }
}
