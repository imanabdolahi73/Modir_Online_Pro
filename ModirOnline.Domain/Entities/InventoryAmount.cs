using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class InventoryAmount
{
    public int InventoryAmountId { get; set; }

    public int? InventoryId { get; set; }

    public int? MaterialId { get; set; }

    public int? LastPrice { get; set; }

    public int? AveragePrice { get; set; }

    public int? Amount { get; set; }

    public int? MaxAmount { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual Material? Material { get; set; }
}
