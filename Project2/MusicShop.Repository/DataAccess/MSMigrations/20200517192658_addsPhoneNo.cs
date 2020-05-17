using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShop.Repository.DataAccess.MSMigrations
{
    public partial class addsPhoneNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    PhoneNo = table.Column<string>(maxLength: 128, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    UserTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 128, nullable: false),
                    City = table.Column<string>(maxLength: 128, nullable: false),
                    State = table.Column<string>(maxLength: 128, nullable: false),
                    Zip = table.Column<string>(maxLength: 128, nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTimeOffset>(nullable: true),
                    StoreId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    InventoryItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    LoggedUserId = table.Column<int>(nullable: false),
                    ChangedDate = table.Column<DateTimeOffset>(nullable: true),
                    StoreId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventory_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ShoppingCartId = table.Column<string>(nullable: true),
                    StoreId = table.Column<string>(nullable: true),
                    customerEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNo", "UserTypeId" },
                values: new object[,]
                {
                    { 1, "sulav.aryal@outlook.com", "Sulav", "Aryal", "password", "1111122221", 1 },
                    { 20, "omelrosej@artisteer.com", "Serena", "San", "password", "1111122240", 2 },
                    { 19, "jarnaudini@webmd.com", "Rosana", "Purvis", "password", "1111122239", 2 },
                    { 17, "susy@outlook.com", "Susy", "Argo", "password", "1111122237", 2 },
                    { 16, "ewigginf@skyrock.com", "Mireya", "Pierro", "password", "1111122236", 2 },
                    { 15, "nellyatte@homestead.com", "Hans", "Spurgin", "password", "1111122235", 2 },
                    { 14, "aharborowd@nbcnews.com", "Moises", "Meche", "password", "1111122234", 2 },
                    { 13, "bianizzic@wisc.edu", "Taneka", "Ord", "password", "1111122233", 2 },
                    { 12, "cbmccaughenb@umn.com", "Gigi", "Degree", "password", "1111122232", 2 },
                    { 11, "ymartyna@ebay.com", "Lucilla", "Chang", "password", "1111122231", 2 },
                    { 18, "mpeyroh@foxnews.com", "Althea", "Dent", "password", "1111122238", 2 },
                    { 9, "gdibdale8@nih.gov", "Mirian", "Stroda", "password", "1111122229", 2 },
                    { 8, "dmagrane7@dagondesign.com", "Jeana", "Dunston", "password", "1111122228", 2 },
                    { 7, "aasken6@etsy.com", "Barret", "Waltrip", "password", "1111122227", 2 },
                    { 6, "igallaccio5@tmall.com", "Maribeth", "Fontenot", "password", "1111122226", 2 },
                    { 5, "mfonzone4@vk.com", "Kenneth", "Windsor", "password", "1111122225", 2 },
                    { 4, "tscurrell3@reuters.com", "Bettie", "Turek", "password", "1111122224", 2 },
                    { 3, "acloney2@dropbox.com", "Brigitte", "Laufer", "password", "1111122223", 2 },
                    { 2, "customer@outlook.com", "Danyelle", "Tsosie", "password", "1111122222", 2 },
                    { 10, "acockran9@arizona.edu", "Beverley", "Digangi", "password", "1111122230", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "ProductCode", "StoreId" },
                values: new object[,]
                {
                    { 10, "Saxophone", 150.55m, "P00010", null },
                    { 9, "Ukulele", 150.55m, "P00009", null },
                    { 8, "Bagpipes", 150.55m, "P00008", null },
                    { 7, "Guitar", 150.55m, "P00007", null },
                    { 6, "Violin", 150.55m, "P00006", null },
                    { 3, "Accordian", 150.55m, "P00003", null },
                    { 4, "Piccolo", 150.55m, "P00004", null },
                    { 2, "Flute", 150.55m, "P00002", null },
                    { 1, "Piano", 150.55m, "P00001", null },
                    { 5, "Trombone", 150.55m, "P00005", null }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Washington" },
                    { 1, "Florida" },
                    { 2, "New York" },
                    { 3, "Texas" },
                    { 5, "California" }
                });

            migrationBuilder.InsertData(
                table: "CustomerAddress",
                columns: new[] { "Id", "City", "CustomerId", "State", "Street", "Zip" },
                values: new object[,]
                {
                    { 3, "Fort Worth", 1, "TX", "96 Franklin Ave.", "76110" },
                    { 20, "Cedar Rapids", 20, "AZ", "8471 East Brandywine Street", "52402" },
                    { 19, "Saint Augustine", 19, "FL", "2 State St.", "32084" },
                    { 18, "Eastpointe", 18, "MI", "58 Fifth St.", "48021" },
                    { 17, "Canandaigua", 17, "NY", "206 New Saddle Ave.", "14424" },
                    { 15, "Lancaster", 15, "NY", "41 Buckingham Ave", "14086" },
                    { 14, "Meadow", 14, "NJ", "48 W. Oak St.", "08003" },
                    { 13, "Huntington", 13, "NY", "467 South Smoky Hollow St", "11743" },
                    { 12, "Munster", 12, "IN", "265 Prairie St.", "46321" },
                    { 11, "Wenatchee", 11, "WA", "3 Myers Street", "98801" },
                    { 16, "Manahawkin", 16, "NJ", "290 Marsh St. ", "08050" },
                    { 9, "Roseville", 9, "MI", "84 Woodsman St.", "48066" },
                    { 8, "West Palm Beach", 8, "FL", "37 Pilgrim Lane", "33404" },
                    { 7, "Missoula", 7, "MT", "580 West Deerfield Road", "59801" },
                    { 1, "Aberdeen", 6, "SD", "67 Carriage Drive", "57401" },
                    { 6, "Belleville", 5, "NJ", "6 College St.", "07109" },
                    { 5, "Gastonia", 4, "NC", "7518 Sherwood Street", "28052" },
                    { 4, "Maplewood", 3, "NJ", "752 South Main Drive", "07040" },
                    { 2, "Green Bay", 2, "WI", "17 Johnson Street", "54302" },
                    { 10, "Green Cove Springs", 10, "FL", "89 North Devonshire Dr", "32043" }
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "ChangedDate", "LoggedUserId", "ProductId", "Quantity", "StoreId" },
                values: new object[,]
                {
                    { 35, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2688), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 4 },
                    { 34, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2682), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 4 },
                    { 33, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2676), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 4 },
                    { 32, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2670), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 4 },
                    { 31, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2665), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 4 },
                    { 27, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2642), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 3 },
                    { 29, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2653), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 3 },
                    { 28, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2648), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 3 },
                    { 26, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2636), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 3 },
                    { 36, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2693), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 4 },
                    { 30, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2659), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 3 },
                    { 37, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2699), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 4 },
                    { 47, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2757), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 5 },
                    { 39, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2711), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 4 },
                    { 40, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2717), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 4 },
                    { 41, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2722), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 5 },
                    { 42, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2728), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 5 },
                    { 43, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2734), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 5 },
                    { 44, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2740), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 5 },
                    { 45, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2745), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 5 },
                    { 46, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2751), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 5 },
                    { 25, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2630), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 3 },
                    { 48, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2763), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 5 },
                    { 49, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2768), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 5 },
                    { 38, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2705), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 4 },
                    { 24, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2624), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 3 },
                    { 16, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2578), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 2 },
                    { 22, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2613), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 3 },
                    { 7, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2526), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 1 },
                    { 8, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2532), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 1 },
                    { 9, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2538), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 1 },
                    { 4, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2508), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 1 },
                    { 10, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2544), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 1 },
                    { 3, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2500), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 1 },
                    { 23, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2618), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 3 },
                    { 11, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2549), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 2 },
                    { 12, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 2 },
                    { 6, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2520), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 1 },
                    { 13, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2561), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 2 },
                    { 15, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2572), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 2 },
                    { 50, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2774), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 5 },
                    { 17, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2584), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 2 },
                    { 18, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2590), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 2 },
                    { 19, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2596), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 2 },
                    { 20, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2601), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 2 },
                    { 2, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2457), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 1 },
                    { 1, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 144, DateTimeKind.Unspecified).AddTicks(3135), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 1 },
                    { 21, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2607), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 3 },
                    { 14, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2566), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 2 },
                    { 5, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(2515), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "StoreId" },
                values: new object[,]
                {
                    { 2, 1, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5374), new TimeSpan(0, -7, 0, 0, 0)), 1 },
                    { 1, 1, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5012), new TimeSpan(0, -7, 0, 0, 0)), 1 },
                    { 3, 2, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, -7, 0, 0, 0)), 2 },
                    { 4, 3, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5397), new TimeSpan(0, -7, 0, 0, 0)), 2 },
                    { 5, 4, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5402), new TimeSpan(0, -7, 0, 0, 0)), 4 },
                    { 6, 5, new DateTimeOffset(new DateTime(2020, 5, 17, 12, 26, 58, 146, DateTimeKind.Unspecified).AddTicks(5408), new TimeSpan(0, -7, 0, 0, 0)), 5 }
                });

            migrationBuilder.InsertData(
                table: "OrderLineItems",
                columns: new[] { "Id", "InventoryItemId", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 150.55m, 2 },
                    { 2, 2, 1, 150.55m, 4 },
                    { 3, 3, 1, 150.55m, 5 },
                    { 4, 4, 1, 150.55m, 7 },
                    { 5, 1, 2, 150.55m, 3 },
                    { 6, 1, 3, 150.55m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StoreId",
                table: "Inventory",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItems_OrderId",
                table: "OrderLineItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "OrderLineItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
