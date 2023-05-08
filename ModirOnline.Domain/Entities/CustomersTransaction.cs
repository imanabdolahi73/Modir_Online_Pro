using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class CustomersTransaction
{
    public int TransactionId { get; set; }

    public string? Type { get; set; }

    public string? Title { get; set; }

    public long? Amount { get; set; }

    public int? EmployeeId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime? DateTime { get; set; }

    public long? BalanceInLine { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
