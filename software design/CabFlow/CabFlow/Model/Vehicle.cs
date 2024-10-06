using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int CategoryId { get; set; }
        public string Info => $"{Manufacturer} {Model} {Year} {LicensePlate}";

        #region EF Navigation
        public Category Category { get; set; }
        public List<Order> Orders { get; set; }

        #endregion

    }
}
