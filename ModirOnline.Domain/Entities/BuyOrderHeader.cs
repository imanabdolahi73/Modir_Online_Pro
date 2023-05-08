using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class BuyOrderHeader
{
    public Guid BuyOrderHeaderId { get; set; }

    public int? SupplierId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? DateTime { get; set; }

    public long? TotalPrice { get; set; }

    public long? Discount { get; set; }

    public long? FinalPrice { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; } = new List<BuyOrderDetail>();

    public virtual Employee? Employee { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
