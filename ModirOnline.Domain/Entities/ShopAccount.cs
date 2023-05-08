using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class ShopAccount
{
    public int ShopAccountId { get; set; }

    public string? AccountNumber { get; set; }

    public string? Title { get; set; }

    public string? BankName { get; set; }

    public long? Ballance { get; set; }

    public virtual ICollection<Cost> Costs { get; } = new List<Cost>();

    public virtual ICollection<ShopAccountsTransaction> ShopAccountsTransactions { get; } = new List<ShopAccountsTransaction>();
}
