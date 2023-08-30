using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7040")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

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

//Add for Cors 
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CHECK IF USER EXISTS

app.MapGet("/api/checkuser/{authId}", (BangazonDbContext db, string authId) => 
{
    var authUser = db.Users.Where(u => u.FBkey == authId).FirstOrDefault();
    if (authUser != null)
    {
        return Results.NotFound();
    }
    return Results.Ok(authUser);
});

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

app.MapGet("/api/prodwithtype/{id}", (BangazonDbContext db, int id) =>
{
    Product product = db.Products.Include(p => p.ProductType).Single(p => p.Id == id);
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
    prodToUpdate.productTypeId = product.productTypeId;
    prodToUpdate.ProductName = product.ProductName;
    prodToUpdate.ProductPrice = product.ProductPrice;
    prodToUpdate.userId = product.userId;
    db.SaveChanges();
    return Results.Ok(prodToUpdate);
});

//orders on products
app.MapGet("/api/products/{id}/orders", (BangazonDbContext db, int id) =>
{
    var product = db.Products.Include(p => p.Orders).FirstOrDefault(p => p.Id == id);
    return product;
});

// Orders CRUD endpoints
app.MapGet("/api/orders", (BangazonDbContext db) =>
{
    return db.Orders.ToList();
});

//products listed on an order
app.MapGet("/api/orders/{id}/products", (BangazonDbContext db, int id) =>
{
    var Order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
    return Order;
});

//add product to an order
app.MapPost("/api/order", (BangazonDbContext db, int orderId, int prodId) =>
{
    var orderToUpdate = db.Orders.FirstOrDefault(o => o.Id == orderId);
    var prodToAdd = db.Products.FirstOrDefault(p => p.Id == prodId);

    orderToUpdate.Products.Add(prodToAdd);

    db.SaveChanges();
    return orderToUpdate;
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
    prodToUpdate.productTypeId = product.productTypeId;
    prodToUpdate.ProductName = product.ProductName;
    prodToUpdate.ProductPrice = product.ProductPrice;
    prodToUpdate.userId = product.userId;
    db.SaveChanges();
    return Results.Ok(prodToUpdate);
});

// Product Type CRUD endpoints
app.MapGet("/api/prodtypes", (BangazonDbContext db) =>
{
    return db.ProductTypes.ToList();
});

app.MapGet("/api/prodtypes/{id}", (BangazonDbContext db, int id) =>
{
    ProductType prodtype = db.ProductTypes.SingleOrDefault(pt => pt.Id == id);
    if (prodtype == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(prodtype);
});

app.MapDelete("/api/prodtypes/{id}", (BangazonDbContext db, int id) =>
{
    ProductType prodtypeToDelete = db.ProductTypes.SingleOrDefault(pt => pt.Id == id);
    if (prodtypeToDelete == null)
    {
        return Results.NotFound();
    }
    db.ProductTypes.Remove(prodtypeToDelete);
    db.SaveChanges();
    return Results.Ok(db.ProductTypes);
});

app.MapPut("/api/prodtypes/{id}", (BangazonDbContext db, int id, ProductType prodtype) =>
{
    ProductType prodtypeToUpdate = db.ProductTypes.SingleOrDefault(pt => pt.Id == id);
    if (prodtypeToUpdate == null)
    {
        return Results.NotFound();
    }
    prodtypeToUpdate.Type = prodtype.Type;
    db.SaveChanges();
    return Results.Ok(prodtypeToUpdate);
});

app.MapPost("/api/prodtypes", (BangazonDbContext db, ProductType prodtype) =>
{
    db.ProductTypes.Add(prodtype);
    db.SaveChanges();
    return Results.Created($"/api/products/{prodtype.Id}", prodtype);
});

// Payment Type CRUD endpoints
app.MapGet("/api/paytypes", (BangazonDbContext db) =>
{
    return db.PaymentTypes.ToList();
});

app.MapGet("/api/paytypes/{id}", (BangazonDbContext db, int id) =>
{
    PaymentType paytype = db.PaymentTypes.SingleOrDefault(pt => pt.Id == id);
    if (paytype == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(paytype);
});

app.MapDelete("/api/paytypes/{id}", (BangazonDbContext db, int id) =>
{
    PaymentType payTypeToDelete = db.PaymentTypes.SingleOrDefault(pt => pt.Id == id);
    if (payTypeToDelete == null)
    {
        return Results.NotFound();
    }
    db.PaymentTypes.Remove(payTypeToDelete);
    db.SaveChanges();
    return Results.Ok(db.PaymentTypes);
});

app.MapPut("/api/paytypes/{id}", (BangazonDbContext db, int id, PaymentType paytype) =>
{
    ProductType paytypeToUpdate = db.ProductTypes.SingleOrDefault(pt => pt.Id == id);
    if (paytypeToUpdate == null)
    {
        return Results.NotFound();
    }
    paytypeToUpdate.Type = paytype.Type;
    db.SaveChanges();
    return Results.Ok(paytypeToUpdate);
});

app.MapPost("/api/paytypes", (BangazonDbContext db, PaymentType paytype) =>
{
    db.PaymentTypes.Add(paytype);
    db.SaveChanges();
    return Results.Created($"/api/products/{paytype.Id}", paytype);
});

app.Run();

