﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bangazon.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    statusId = table.Column<int>(type: "integer", nullable: false),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    orderTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    paymentType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productType = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPmtTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPmtTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FBkey = table.Column<string>(type: "text", nullable: false),
                    isSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "status 1" },
                    { 2, "status 2" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "orderTotal", "paymentType", "productId", "statusId", "userId" },
                values: new object[,]
                {
                    { 1, 10.00m, 1, 1, 1, 1 },
                    { 2, 20.00m, 2, 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "payment type 1" },
                    { 2, "payment type 2" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "prod type 1" },
                    { 2, "prod type 2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ProductName", "ProductPrice", "productType", "userId" },
                values: new object[,]
                {
                    { 1, "product 1", 10.00m, 1, 1 },
                    { 2, "product 2", 20.00m, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserPmtTypes",
                columns: new[] { "Id", "PaymentTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FBkey", "Name", "isSeller" },
                values: new object[,]
                {
                    { 1, "", "User1", true },
                    { 2, "", "User2", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "UserPmtTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
