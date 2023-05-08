using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class MaterialUsed
{
    public int MaterialUsedId { get; set; }

    public int? MaterialId { get; set; }

    public int? ProductId { get; set; }

    public string? Title { get; set; }

    public int? Amount { get; set; }

    public virtual Material? Material { get; set; }

    public virtual Product? Product { get; set; }
}
