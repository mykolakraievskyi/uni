using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class Customer
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly ClientFrom { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
