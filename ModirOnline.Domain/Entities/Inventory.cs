using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public string? Title { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<InOutMaterial> InOutMaterials { get; } = new List<InOutMaterial>();

    public virtual ICollection<InventoryAmount> InventoryAmounts { get; } = new List<InventoryAmount>();
}
