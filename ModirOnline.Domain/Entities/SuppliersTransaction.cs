using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class SuppliersTransaction
{
    public int TransactionId { get; set; }

    public string? Type { get; set; }

    public string? Title { get; set; }

    public long? Amount { get; set; }

    public int? SupplierId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? DateTime { get; set; }

    public long? BalanceInLine { get; set; }

    public string? Status { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
