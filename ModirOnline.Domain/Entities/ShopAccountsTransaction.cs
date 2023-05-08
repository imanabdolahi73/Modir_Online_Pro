using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class ShopAccountsTransaction
{
    public int TransactionId { get; set; }

    public string? Type { get; set; }

    public string? Title { get; set; }

    public long? Amount { get; set; }

    public int? EmployeeId { get; set; }

    public int? ShopAccountId { get; set; }

    public DateTime? DateTime { get; set; }

    public long? BalanceInLine { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ShopAccount? ShopAccount { get; set; }
}
