using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class Point
{
    public int Id { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Trip> TripEndPoints { get; set; } = new List<Trip>();

    public virtual ICollection<Trip> TripStartPoints { get; set; } = new List<Trip>();
}
