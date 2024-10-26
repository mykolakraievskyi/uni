﻿using System;
using System.Collections.Generic;

namespace TaxiDbFirst;

public partial class NewDriver
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public double? Rating { get; set; }

    public DateOnly StartedWorkingOn { get; set; }
}
