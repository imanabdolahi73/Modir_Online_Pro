using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class InOutMaterial
{
    public int InOutId { get; set; }

    public string? Type { get; set; }

    public string? Title { get; set; }

    public int? MaterialId { get; set; }

    public int? InventoryId { get; set; }

    public int? EmployeeId { get; set; }
    
    public int? Price { get; set; }

    public DateTime? DateTime { get; set; }

    public int? Amount { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual Material? Material { get; set; }
}
