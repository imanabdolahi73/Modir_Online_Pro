using System;
using System.Collections.Generic;

namespace ModirOnline.Domain.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Post { get; set; }

    public int? AccessLevelId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? Visible { get; set; }

    public long? Balance { get; set; }

    public string? Status { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<BuyOrderHeader> BuyOrderHeaders { get; } = new List<BuyOrderHeader>();

    public virtual ICollection<Cost> Costs { get; } = new List<Cost>();

    public virtual ICollection<CustomersTransaction> CustomersTransactions { get; } = new List<CustomersTransaction>();

    public virtual ICollection<EmployeesTransaction> EmployeesTransactionEmployees { get; } = new List<EmployeesTransaction>();

    public virtual ICollection<EmployeesTransaction> EmployeesTransactionRegEmployees { get; } = new List<EmployeesTransaction>();

    public virtual ICollection<InOutMaterial> InOutMaterials { get; } = new List<InOutMaterial>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ShopAccountsTransaction> ShopAccountsTransactions { get; } = new List<ShopAccountsTransaction>();

    public virtual ICollection<SuppliersTransaction> SuppliersTransactions { get; } = new List<SuppliersTransaction>();
}
