using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Material
{
    public int MaterialId { get; set; }

    public string? Title { get; set; }

    public string? Unit { get; set; }

    public int? MaterialCategoryId { get; set; }

    public int? Visible { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; } = new List<BuyOrderDetail>();

    public virtual ICollection<InOutMaterial> InOutMaterials { get; } = new List<InOutMaterial>();

    public virtual ICollection<InventoryAmount> InventoryAmounts { get; } = new List<InventoryAmount>();

    public virtual MaterialCategory? MaterialCategory { get; set; }

    public virtual ICollection<MaterialUsed> MaterialUseds { get; } = new List<MaterialUsed>();
}
