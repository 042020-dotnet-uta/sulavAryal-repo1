using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShop.Repository.DataAccess.MSMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 128, nullable: true),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 128, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    UserTypeId = table.Column<int>(nullable: false)
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
                    Street = table.Column<string>(maxLength: 128, nullable: true),
                    City = table.Column<string>(maxLength: 128, nullable: true),
                    State = table.Column<string>(maxLength: 128, nullable: true),
                    Zip = table.Column<string>(maxLength: 128, nullable: true),
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNo", "UserTypeId" },
                values: new object[,]
                {
                    { 1, "sulav.aryal@outlook.com", "Sulav", "Aryal", "password", null, 1 },
                    { 20, "omelrosej@artisteer.com", "Serena", "San", "password", null, 2 },
                    { 19, "jarnaudini@webmd.com", "Rosana", "Purvis", "password", null, 2 },
                    { 17, "susy@outlook.com", "Susy", "Argo", "password", null, 2 },
                    { 16, "ewigginf@skyrock.com", "Mireya", "Pierro", "password", null, 2 },
                    { 15, "nellyatte@homestead.com", "Hans", "Spurgin", "password", null, 2 },
                    { 14, "aharborowd@nbcnews.com", "Moises", "Meche", "password", null, 2 },
                    { 13, "bianizzic@wisc.edu", "Taneka", "Ord", "password", null, 2 },
                    { 12, "cbmccaughenb@umn.com", "Gigi", "Degree", "password", null, 2 },
                    { 11, "ymartyna@ebay.com", "Lucilla", "Chang", "password", null, 2 },
                    { 18, "mpeyroh@foxnews.com", "Althea", "Dent", "password", null, 2 },
                    { 9, "gdibdale8@nih.gov", "Mirian", "Stroda", "password", null, 2 },
                    { 8, "dmagrane7@dagondesign.com", "Jeana", "Dunston", "password", null, 2 },
                    { 7, "aasken6@etsy.com", "Barret", "Waltrip", "password", null, 2 },
                    { 6, "igallaccio5@tmall.com", "Maribeth", "Fontenot", "password", null, 2 },
                    { 5, "mfonzone4@vk.com", "Kenneth", "Windsor", "password", null, 2 },
                    { 4, "tscurrell3@reuters.com", "Bettie", "Turek", "password", null, 2 },
                    { 3, "acloney2@dropbox.com", "Brigitte", "Laufer", "password", null, 2 },
                    { 2, "dcove@networking.org", "Danyelle", "Tsosie", "password", null, 2 },
                    { 10, "acockran9@arizona.edu", "Beverley", "Digangi", "password", null, 2 }
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
                    { 35, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(647), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 4 },
                    { 34, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(641), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 4 },
                    { 33, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(635), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 4 },
                    { 32, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 4 },
                    { 31, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(624), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 4 },
                    { 27, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(600), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 3 },
                    { 29, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(612), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 3 },
                    { 28, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(606), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 3 },
                    { 26, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(595), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 3 },
                    { 36, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(652), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 4 },
                    { 30, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(618), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 3 },
                    { 37, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(658), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 4 },
                    { 47, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(715), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 5 },
                    { 39, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(669), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 4 },
                    { 40, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(675), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 4 },
                    { 41, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(681), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 5 },
                    { 42, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(686), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 5 },
                    { 43, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(692), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 5 },
                    { 44, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 5 },
                    { 45, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(703), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 5 },
                    { 46, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(709), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 5 },
                    { 25, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(589), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 3 },
                    { 48, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(757), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 5 },
                    { 49, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(764), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 5 },
                    { 38, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(664), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 4 },
                    { 24, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(583), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 3 },
                    { 16, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(538), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 2 },
                    { 22, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(572), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 3 },
                    { 7, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(486), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 1 },
                    { 8, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(491), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 1 },
                    { 9, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(497), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 1 },
                    { 4, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(467), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 1 },
                    { 10, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(503), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 1 },
                    { 3, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(459), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 1 },
                    { 23, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(578), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 3 },
                    { 11, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(509), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 2 },
                    { 12, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(515), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 2 },
                    { 6, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(479), new TimeSpan(0, -7, 0, 0, 0)), 1, 6, 20, 1 },
                    { 13, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(520), new TimeSpan(0, -7, 0, 0, 0)), 1, 3, 20, 2 },
                    { 15, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(532), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 2 },
                    { 50, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(769), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 5 },
                    { 17, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(543), new TimeSpan(0, -7, 0, 0, 0)), 1, 7, 20, 2 },
                    { 18, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(549), new TimeSpan(0, -7, 0, 0, 0)), 1, 8, 20, 2 },
                    { 19, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(555), new TimeSpan(0, -7, 0, 0, 0)), 1, 9, 20, 2 },
                    { 20, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(560), new TimeSpan(0, -7, 0, 0, 0)), 1, 10, 20, 2 },
                    { 2, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(418), new TimeSpan(0, -7, 0, 0, 0)), 1, 2, 20, 1 },
                    { 1, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 315, DateTimeKind.Unspecified).AddTicks(3682), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 1 },
                    { 21, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(566), new TimeSpan(0, -7, 0, 0, 0)), 1, 1, 20, 3 },
                    { 14, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(526), new TimeSpan(0, -7, 0, 0, 0)), 1, 4, 20, 2 },
                    { 5, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(473), new TimeSpan(0, -7, 0, 0, 0)), 1, 5, 20, 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "StoreId" },
                values: new object[,]
                {
                    { 2, 1, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, -7, 0, 0, 0)), 1 },
                    { 1, 1, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3085), new TimeSpan(0, -7, 0, 0, 0)), 1 },
                    { 3, 2, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3395), new TimeSpan(0, -7, 0, 0, 0)), 2 },
                    { 4, 3, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3401), new TimeSpan(0, -7, 0, 0, 0)), 2 },
                    { 5, 4, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3407), new TimeSpan(0, -7, 0, 0, 0)), 4 },
                    { 6, 5, new DateTimeOffset(new DateTime(2020, 5, 13, 23, 39, 56, 317, DateTimeKind.Unspecified).AddTicks(3413), new TimeSpan(0, -7, 0, 0, 0)), 5 }
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
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
