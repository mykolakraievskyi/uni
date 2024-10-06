using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class Trip
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public DateOnly PickUpTime { get; set; }

    public int StartPointId { get; set; }

    public int EndPointId { get; set; }

    public double? Cost { get; set; }

    public int? Rating { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual Point EndPoint { get; set; } = null!;

    public virtual Point StartPoint { get; set; } = null!;

    public virtual TripStatus Status { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
