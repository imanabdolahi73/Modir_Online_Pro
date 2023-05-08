using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class MaterialCategory
{
    public int MaterialCategoryId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
