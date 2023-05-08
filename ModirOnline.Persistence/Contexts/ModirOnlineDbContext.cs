using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Domain.Entities;

namespace ModirOnline.Persistence.Contexts;

public partial class ModirOnlineDbContext : DbContext , IModirOnlineDbContext
{
    public ModirOnlineDbContext()
    {
    }

    public ModirOnlineDbContext(DbContextOptions<ModirOnlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuyOrderDetail> BuyOrderDetails { get; set; }

    public virtual DbSet<BuyOrderHeader> BuyOrderHeaders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Cost> Costs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomersTransaction> CustomersTransactions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesTransaction> EmployeesTransactions { get; set; }

    public virtual DbSet<InOutMaterial> InOutMaterials { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryAmount> InventoryAmounts { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialCategory> MaterialCategories { get; set; }

    public virtual DbSet<MaterialUsed> MaterialUseds { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShopAccount> ShopAccounts { get; set; }

    public virtual DbSet<ShopAccountsTransaction> ShopAccountsTransactions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SuppliersTransaction> SuppliersTransactions { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IQ90JPA;Database=ModirOnline_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuyOrderDetail>(entity =>
        {
            entity.Property(e => e.BuyOrderDetailId).HasColumnName("BuyOrderDetailID");
            entity.Property(e => e.BuyOrderHeaderId).HasColumnName("BuyOrderHeaderID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.BuyOrderHeader).WithMany(p => p.BuyOrderDetails)
                .HasForeignKey(d => d.BuyOrderHeaderId)
                .HasConstraintName("FK_BuyOrderDetails_BuyOrderHeaders");

            entity.HasOne(d => d.Material).WithMany(p => p.BuyOrderDetails)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_BuyOrderDetails_Materials");
        });

        modelBuilder.Entity<BuyOrderHeader>(entity =>
        {
            entity.Property(e => e.BuyOrderHeaderId)
                .ValueGeneratedNever()
                .HasColumnName("BuyOrderHeaderID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Employee).WithMany(p => p.BuyOrderHeaders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BuyOrderHeaders_Employees");

            entity.HasOne(d => d.Supplier).WithMany(p => p.BuyOrderHeaders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_BuyOrderHeaders_Suppliers");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Categories_Categories");
        });

        modelBuilder.Entity<Cost>(entity =>
        {
            entity.Property(e => e.CostId).HasColumnName("CostID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ShopAccountId).HasColumnName("ShopAccountID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Costs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Costs_Employees");

            entity.HasOne(d => d.ShopAccount).WithMany(p => p.Costs)
                .HasForeignKey(d => d.ShopAccountId)
                .HasConstraintName("FK_Costs_ShopAccounts");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Customers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Customers_Roles");
        });

        modelBuilder.Entity<CustomersTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomersTransactions_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.CustomersTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_CustomersTransactions_Employees");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Employees_Roles");
        });

        modelBuilder.Entity<EmployeesTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RegEmployeeId).HasColumnName("RegEmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesTransactionEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeesTransactions_Employees");

            entity.HasOne(d => d.RegEmployee).WithMany(p => p.EmployeesTransactionRegEmployees)
                .HasForeignKey(d => d.RegEmployeeId)
                .HasConstraintName("FK_EmployeesTransactions_Employees1");
        });

        modelBuilder.Entity<InOutMaterial>(entity =>
        {
            entity.HasKey(e => e.InOutId);

            entity.Property(e => e.InOutId).HasColumnName("InOutID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Employee).WithMany(p => p.InOutMaterials)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_InOutMaterials_Employees");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InOutMaterials)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_InOutMaterials_Inventories");

            entity.HasOne(d => d.Material).WithMany(p => p.InOutMaterials)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_InOutMaterials_Materials");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
        });

        modelBuilder.Entity<InventoryAmount>(entity =>
        {
            entity.ToTable("InventoryAmount");

            entity.Property(e => e.InventoryAmountId).HasColumnName("InventoryAmountID");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryAmounts)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_InventoryAmount_Inventories");

            entity.HasOne(d => d.Material).WithMany(p => p.InventoryAmounts)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_InventoryAmount_Materials");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.MaterialCategoryId).HasColumnName("MaterialCategoryID");

            entity.HasOne(d => d.MaterialCategory).WithMany(p => p.Materials)
                .HasForeignKey(d => d.MaterialCategoryId)
                .HasConstraintName("FK_Materials_MaterialCategories");
        });

        modelBuilder.Entity<MaterialCategory>(entity =>
        {
            entity.Property(e => e.MaterialCategoryId).HasColumnName("MaterialCategoryID");
        });

        modelBuilder.Entity<MaterialUsed>(entity =>
        {
            entity.ToTable("MaterialUsed");

            entity.Property(e => e.MaterialUsedId).HasColumnName("MaterialUsedID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialUseds)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_MaterialUsed_Materials");

            entity.HasOne(d => d.Product).WithMany(p => p.MaterialUseds)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_MaterialUsed_Products");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_OrderHeaders");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.AccountantConfirmDateTime).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryConfirmDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeliveryRequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.OrderConfirmDateTime).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });

        modelBuilder.Entity<ShopAccount>(entity =>
        {
            entity.Property(e => e.ShopAccountId).HasColumnName("ShopAccountID");
        });

        modelBuilder.Entity<ShopAccountsTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ShopAccountId).HasColumnName("ShopAccountID");

            entity.HasOne(d => d.Employee).WithMany(p => p.ShopAccountsTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ShopAccountsTransactions_Employees");

            entity.HasOne(d => d.ShopAccount).WithMany(p => p.ShopAccountsTransactions)
                .HasForeignKey(d => d.ShopAccountId)
                .HasConstraintName("FK_ShopAccountsTransactions_ShopAccounts");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
        });

        modelBuilder.Entity<SuppliersTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Employee).WithMany(p => p.SuppliersTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_SuppliersTransactions_Employees");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SuppliersTransactions)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_SuppliersTransactions_Suppliers");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.Property(e => e.TableId).HasColumnName("TableID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
