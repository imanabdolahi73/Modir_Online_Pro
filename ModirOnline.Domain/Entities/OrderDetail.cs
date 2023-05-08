using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public Guid? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Number { get; set; }

    public long? Price { get; set; }

    public long? Discount { get; set; }

    public string? DiscountReason { get; set; }

    public long? ExtraPay { get; set; }

    public string? ExtraPayReason { get; set; }

    public int? Tax { get; set; }

    public long? TotalPrice { get; set; }

    public long? FinalPrice { get; set; }

    public long? CostPrice { get; set; }

    public long? ProfitPrice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
