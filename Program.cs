using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// User CRUD Endpoints

app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

app.MapPost("/api/users", (BangazonDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    User user = db.Users.SingleOrDefault(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

app.MapDelete("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    User userToDelete = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToDelete == null)
    {
        return Results.NotFound();
    }
    db.Users.Remove(userToDelete);
    db.SaveChanges();
    return Results.Ok(db.Users);
});

app.MapPut("/api/users/{id}", (BangazonDbContext db, int id, User user) =>
{
    User userToUpdate = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }
    userToUpdate.Name = user.Name;
    userToUpdate.FBkey = user.FBkey;
    userToUpdate.isSeller = user.isSeller;
    db.SaveChanges();
    return Results.Ok(userToUpdate);
});

// Product CRUD endpoints

app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

app.MapPost("/api/products", (BangazonDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    Product product = db.Products.SingleOrDefault(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});

app.MapDelete("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    Product productToDelete = db.Products.SingleOrDefault(p => p.Id == id);
    if (productToDelete == null)
    {
        return Results.NotFound();
    }
    db.Products.Remove(productToDelete);
    db.SaveChanges();
    return Results.Ok(db.Products);
});

app.MapPut("/api/products/{id}", (BangazonDbContext db, int id, Product product) =>
{
    Product prodToUpdate = db.Products.SingleOrDefault(p => p.Id == id);
    if (prodToUpdate == null)
    {
        return Results.NotFound();
    }
    prodToUpdate.productType = product.productType;
    prodToUpdate.ProductName = product.ProductName;
    prodToUpdate.ProductPrice = product.ProductPrice;
    prodToUpdate.userId = product.userId;
    db.SaveChanges();
    return Results.Ok(prodToUpdate);
});

// Orders CRUD endpoints
app.MapGet("/api/orders", (BangazonDbContext db) =>
{
    return db.Orders.ToList();
});

app.MapPost("/api/Orders", (BangazonDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/products/{order.Id}", order);
});

app.MapGet("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(p => p.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(order);
});

app.MapDelete("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    Order orderToDelete = db.Orders.SingleOrDefault(p => p.Id == id);
    if (orderToDelete == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(orderToDelete);
    db.SaveChanges();
    return Results.Ok(db.Orders);
});

app.MapPut("/api/orders/{id}", (BangazonDbContext db, int id, Product product) =>
{
    Product prodToUpdate = db.Products.SingleOrDefault(p => p.Id == id);
    if (prodToUpdate == null)
    {
        return Results.NotFound();
    }
    prodToUpdate.productType = product.productType;
    prodToUpdate.ProductName = product.ProductName;
    prodToUpdate.ProductPrice = product.ProductPrice;
    prodToUpdate.userId = product.userId;
    db.SaveChanges();
    return Results.Ok(prodToUpdate);
});

app.Run();

