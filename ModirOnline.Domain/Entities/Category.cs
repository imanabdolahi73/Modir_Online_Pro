using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? Title { get; set; }

    public string? PhotoAddress { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
