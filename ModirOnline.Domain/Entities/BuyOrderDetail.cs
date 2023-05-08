using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class BuyOrderDetail
{
    public int BuyOrderDetailId { get; set; }

    public Guid BuyOrderHeaderId { get; set; }

    public int MaterialId { get; set; }

    public int Amount { get; set; }

    public long Price { get; set; }

    public long TotalPrice { get; set; }

    public virtual BuyOrderHeader BuyOrderHeader { get; set; }

    public virtual Material Material { get; set; }
}
