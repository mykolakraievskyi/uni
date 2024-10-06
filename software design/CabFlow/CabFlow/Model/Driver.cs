using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Model
{
    public class Driver
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Number { get; set; }
        [NotMapped]
        public string Fullname => $"{Firstname} {Lastname}";
        public DateTime DateOfBirth { get; set; }
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public float Rating { get; set; }
        public DateTime StartedWorkingOn { get; set; }

        #region EF Navigation

        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }

        #endregion
    }
}
