using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region EF Navigation
        public List<Driver> Drivers { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        #endregion
    }
}
