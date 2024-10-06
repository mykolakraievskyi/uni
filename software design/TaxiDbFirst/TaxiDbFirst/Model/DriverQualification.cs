using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class DriverQualification
{
    public int Id { get; set; }

    public int DriverId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;
}
