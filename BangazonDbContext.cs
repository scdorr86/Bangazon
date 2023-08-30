using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using System.Runtime.CompilerServices;

public class BangazonDbContext : DbContext
{

    public DbSet<Order> Orders { get; set; }
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

        var Order1 = new Order { Id = 1, userId = 1, statusId = 1, paymentType = 1 };
        var Order2 = new Order { Id = 2, userId = 2, statusId = 2, paymentType = 2 };
        var Product1 = new Product { Id = 1, productTypeId = 1, ProductName = "product 1", ProductPrice = 10.00M, userId = 1 };
        var Product2 = new Product { Id = 2, productTypeId = 2, ProductName = "product 2", ProductPrice = 20.00M, userId = 1 };
        var Product3 = new Product { Id = 3, productTypeId = 3, ProductName = "product 3", ProductPrice = 30.00M, userId = 0 };
        var orderProduct = modelBuilder.Entity("OrderProduct");

        //Order1.Products.Add(Product1);
        //Order1.Products.Add(Product2);
        //Order2.Products.Add(Product3);
        //Product1.Orders.Add(Order1);
        //Product2.Orders.Add(Order1);
        //Product3.Orders.Add(Order2);

        modelBuilder.Entity<Order>().HasData(Order1, Order2);

        orderProduct.HasData(
            new { OrdersId = 1, ProductsId = 1 },
            new { OrdersId = 1, ProductsId = 2 },
            new { OrdersId = 2, ProductsId = 3 });


        modelBuilder.Entity<Product>().HasData(Product1,Product2,Product3);

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Name = "User1", FBkey = "", isSeller = true},
            new User {Id = 2, Name = "User2", FBkey = "", isSeller = false}
        });

        modelBuilder.Entity<ProductType>().HasData(new ProductType[]
        {
            new ProductType {Id = 1, Type = "prod type 1"},
            new ProductType {Id = 2, Type = "prod type 2"}
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