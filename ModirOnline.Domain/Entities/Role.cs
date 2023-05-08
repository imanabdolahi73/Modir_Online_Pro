using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
