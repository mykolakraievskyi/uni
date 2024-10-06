using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class Vehicle
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int Year { get; set; }

    public int Seats { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
