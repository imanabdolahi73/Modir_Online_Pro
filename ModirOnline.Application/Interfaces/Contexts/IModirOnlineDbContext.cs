using Microsoft.EntityFrameworkCore;
using ModirOnline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Interfaces.Contexts
{
    public interface IModirOnlineDbContext
    {

        DbSet<BuyOrderDetail> BuyOrderDetails { get; set; }

        DbSet<BuyOrderHeader> BuyOrderHeaders { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Cost> Costs { get; set; }

        DbSet<Customer> Customers { get; set; }

        DbSet<CustomersTransaction> CustomersTransactions { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<EmployeesTransaction> EmployeesTransactions { get; set; }

        DbSet<InOutMaterial> InOutMaterials { get; set; }

        DbSet<Inventory> Inventories { get; set; }

        DbSet<InventoryAmount> InventoryAmounts { get; set; }

        DbSet<Material> Materials { get; set; }

        DbSet<MaterialCategory> MaterialCategories { get; set; }

        DbSet<MaterialUsed> MaterialUseds { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderDetail> OrderDetails { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<Role> Roles { get; set; }

        DbSet<ShopAccount> ShopAccounts { get; set; }

        DbSet<ShopAccountsTransaction> ShopAccountsTransactions { get; set; }

        DbSet<Supplier> Suppliers { get; set; }

        DbSet<SuppliersTransaction> SuppliersTransactions { get; set; }

        DbSet<Table> Tables { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
