using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DriverQualification> DriverQualifications { get; set; } = new List<DriverQualification>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
