using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using System.Runtime.CompilerServices;

public class BangazonDbContext : DbContext
{

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPmtType> UserPmtTypes { get; set; }

    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order {Id = 1, userId = 1, statusId = 1, orderTotal = 10.00M, paymentType = 1},
            new Order {Id = 2, userId = 2, statusId = 2, orderTotal = 20.00M, paymentType = 2}
        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Name = "User1", FBkey = "", isSeller = true},
            new User {Id = 2, Name = "User2", FBkey = "", isSeller = false}
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product {Id = 1, productType = 1, ProductName = "product 1", ProductPrice = 10.00M, userId = 1},
            new Product {Id = 2, productType = 2, ProductName = "product 2", ProductPrice = 20.00M, userId = 1}
        });

        modelBuilder.Entity<ProductType>().HasData(new ProductType[]
        {
            new ProductType {Id = 1, Type = "prod type 1"},
            new ProductType {Id = 2, Type = "prod type 2"}
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct {Id = 1, ProductId = 1, OrderId = 1},
            new OrderProduct {Id = 2, ProductId = 2, OrderId = 2}
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType {Id = 1, Type = "payment type 1"},
            new PaymentType {Id = 2, Type = "payment type 2"}
        });

        modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus[]
        {
            new OrderStatus { Id = 1, Status = "status 1" },
            new OrderStatus { Id = 2, Status = "status 2" }
        });
        modelBuilder.Entity<UserPmtType>().HasData(new UserPmtType[]
        {
            new UserPmtType { Id = 1, UserId = 1, PaymentTypeId = 1 },
            new UserPmtType { Id = 2, UserId = 2, PaymentTypeId = 2 }
        });
    }
}