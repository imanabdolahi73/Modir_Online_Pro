using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime? RegisterDateTime { get; set; }

    public DateTime? OrderConfirmDateTime { get; set; }

    public int? OrderConfirm { get; set; }

    public DateTime? AccountantConfirmDateTime { get; set; }

    public DateTime? DeliveryConfirmDateTime { get; set; }

    public int? DeliveryConfirm { get; set; }

    public string? OrderStatus { get; set; }

    public string? Address { get; set; }

    public DateTime? DeliveryRequestDateTime { get; set; }

    public string? Type { get; set; }

    public int OrderNumber { get; set; }

    public long? Discount { get; set; }

    public string? DiscountReason { get; set; }

    public long? ExtraPay { get; set; }

    public string? ExtraPayReason { get; set; }

    public int? Tax { get; set; }

    public long? TotalPrice { get; set; }

    public long? FinalPrice { get; set; }

    public long? CostPrice { get; set; }

    public long? ProfitPrice { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
