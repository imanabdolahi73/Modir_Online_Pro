using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Title { get; set; }

    public int? CategoryId { get; set; }

    public long? Price { get; set; }

    public string? PhotoAddress { get; set; }

    public int? Visible { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<MaterialUsed> MaterialUseds { get; } = new List<MaterialUsed>();

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
