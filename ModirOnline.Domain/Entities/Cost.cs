using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Cost
{
    public int CostId { get; set; }

    public string? Title { get; set; }

    public long? Amount { get; set; }

    public int? ShopAccountId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ShopAccount? ShopAccount { get; set; }
}
