using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? Type { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public long? Balance { get; set; }

    public string? Status { get; set; }

    public int? StarGrade { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<CustomersTransaction> CustomersTransactions { get; } = new List<CustomersTransaction>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
