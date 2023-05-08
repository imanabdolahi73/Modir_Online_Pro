using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public long? Balance { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<BuyOrderHeader> BuyOrderHeaders { get; } = new List<BuyOrderHeader>();

    public virtual ICollection<SuppliersTransaction> SuppliersTransactions { get; } = new List<SuppliersTransaction>();
}
