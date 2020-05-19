using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using System;

namespace MusicShop.Repository.DataAccess
{
    /// <summary>
    /// Extention for ModelBuild class, 
    /// Extentions are wrappers around builtin methods you want to extend functionality of
    /// </summary>
    public static class ModelBuilderExtensions
    {
        // worked out from url : https://code-maze.com/migrations-and-seed-data-efcore/

        /// <summary>
        /// Seeds data for UserType, Customer, Product, Store, Inventory, Order and OrderLineItem
        /// Keeps onmodel builder override at dbContext cleaner. 
        /// </summary>
        /// <param name="modelBuilder">'this' keyword at the begining makes this a extention of the parameter type</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {


            #region CustomerAddress Seed
            // Seeds UserType data
            modelBuilder.Entity<CustomerAddress>().HasData(
                new CustomerAddress { Id = 3, Street = "96 Franklin Ave.", City = "Fort Worth", State = "TX", Zip = "76110", CustomerId = 1 },
                new CustomerAddress { Id = 2, Street = "17 Johnson Street", City = "Green Bay", State = "WI", Zip = "54302", CustomerId = 2 },
                new CustomerAddress { Id = 4, Street = "752 South Main Drive", City = "Maplewood", State = "NJ", Zip = "07040", CustomerId = 3 },
                new CustomerAddress { Id = 5, Street = "7518 Sherwood Street", City = "Gastonia", State = "NC", Zip = "28052", CustomerId = 4 },
                new CustomerAddress { Id = 6, Street = "6 College St.", City = "Belleville", State = "NJ", Zip = "07109", CustomerId = 5 },
                new CustomerAddress { Id = 1, Street = "67 Carriage Drive", City = "Aberdeen", State = "SD", Zip = "57401", CustomerId = 6 },
                new CustomerAddress { Id = 7, Street = "580 West Deerfield Road", City = "Missoula", State = "MT", Zip = "59801", CustomerId = 7 },
                new CustomerAddress { Id = 8, Street = "37 Pilgrim Lane", City = "West Palm Beach", State = "FL", Zip = "33404", CustomerId = 8 },
                new CustomerAddress { Id = 9, Street = "84 Woodsman St.", City = "Roseville", State = "MI", Zip = "48066", CustomerId = 9 },
                new CustomerAddress { Id = 10, Street = "89 North Devonshire Dr", City = "Green Cove Springs", State = "FL", Zip = "32043", CustomerId = 10 },
                new CustomerAddress { Id = 11, Street = "3 Myers Street", City = "Wenatchee", State = "WA", Zip = "98801", CustomerId = 11 },
                new CustomerAddress { Id = 12, Street = "265 Prairie St.", City = "Munster", State = "IN", Zip = "46321", CustomerId = 12 },
                new CustomerAddress { Id = 13, Street = "467 South Smoky Hollow St", City = "Huntington", State = "NY", Zip = "11743", CustomerId = 13 },
                new CustomerAddress { Id = 14, Street = "48 W. Oak St.", City = "Meadow", State = "NJ", Zip = "08003", CustomerId = 14 },
                new CustomerAddress { Id = 15, Street = "41 Buckingham Ave", City = "Lancaster", State = "NY", Zip = "14086", CustomerId = 15 },
                new CustomerAddress { Id = 16, Street = "290 Marsh St. ", City = "Manahawkin", State = "NJ", Zip = "08050", CustomerId = 16 },
                new CustomerAddress { Id = 17, Street = "206 New Saddle Ave.", City = "Canandaigua", State = "NY", Zip = "14424", CustomerId = 17 },
                new CustomerAddress { Id = 18, Street = "58 Fifth St.", City = "Eastpointe", State = "MI", Zip = "48021", CustomerId = 18 },
                new CustomerAddress { Id = 19, Street = "2 State St.", City = "Saint Augustine", State = "FL", Zip = "32084", CustomerId = 19 },
                new CustomerAddress { Id = 20, Street = "8471 East Brandywine Street", City = "Cedar Rapids", State = "AZ", Zip = "52402", CustomerId = 20 }
                );
            #endregion

            #region Customer Seed
            // Seeds Customer data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Sulav", LastName = "Aryal", PhoneNo = "1111122221", Email = "sulav.aryal@outlook.com", Password = "password", UserTypeId = 1 },
                new Customer { Id = 2, FirstName = "Danyelle", LastName = "Tsosie", PhoneNo = "1111122222", Email = "customer@outlook.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 3, FirstName = "Brigitte", LastName = "Laufer", PhoneNo = "1111122223", Email = "acloney2@dropbox.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 4, FirstName = "Bettie", LastName = "Turek", PhoneNo = "1111122224", Email = "tscurrell3@reuters.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 5, FirstName = "Kenneth", LastName = "Windsor", PhoneNo = "1111122225", Email = "mfonzone4@vk.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 6, FirstName = "Maribeth", LastName = "Fontenot", PhoneNo = "1111122226", Email = "igallaccio5@tmall.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 7, FirstName = "Barret", LastName = "Waltrip", PhoneNo = "1111122227", Email = "aasken6@etsy.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 8, FirstName = "Jeana", LastName = "Dunston", PhoneNo = "1111122228", Email = "dmagrane7@dagondesign.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 9, FirstName = "Mirian", LastName = "Stroda", PhoneNo = "1111122229", Email = "gdibdale8@nih.gov", Password = "password", UserTypeId = 2 },
                new Customer { Id = 10, FirstName = "Beverley", LastName = "Digangi", PhoneNo = "1111122230", Email = "acockran9@arizona.edu", Password = "password", UserTypeId = 2 },
                new Customer { Id = 11, FirstName = "Lucilla", LastName = "Chang", PhoneNo = "1111122231", Email = "ymartyna@ebay.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 12, FirstName = "Gigi", LastName = "Degree", PhoneNo = "1111122232", Email = "cbmccaughenb@umn.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 13, FirstName = "Taneka", LastName = "Ord", PhoneNo = "1111122233", Email = "bianizzic@wisc.edu", Password = "password", UserTypeId = 2 },
                new Customer { Id = 14, FirstName = "Moises", LastName = "Meche", PhoneNo = "1111122234", Email = "aharborowd@nbcnews.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 15, FirstName = "Hans", LastName = "Spurgin", PhoneNo = "1111122235", Email = "nellyatte@homestead.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 16, FirstName = "Mireya", LastName = "Pierro", PhoneNo = "1111122236", Email = "ewigginf@skyrock.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 17, FirstName = "Susy", LastName = "Argo", PhoneNo = "1111122237", Email = "susy@outlook.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 18, FirstName = "Althea", LastName = "Dent", PhoneNo = "1111122238", Email = "mpeyroh@foxnews.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 19, FirstName = "Rosana", LastName = "Purvis", PhoneNo = "1111122239", Email = "jarnaudini@webmd.com", Password = "password", UserTypeId = 2 },
                new Customer { Id = 20, FirstName = "Serena", LastName = "San", PhoneNo = "1111122240", Email = "omelrosej@artisteer.com", Password = "password", UserTypeId = 2 }
            );
            #endregion

            #region Products Seed
            // Seeds Product Names
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Piano", ProductCode = "P00001", Price = 150.55M },
                new Product { Id = 2, Name = "Flute", ProductCode = "P00002", Price = 150.55M },
                new Product { Id = 3, Name = "Accordian", ProductCode = "P00003", Price = 150.55M },
                new Product { Id = 4, Name = "Piccolo", ProductCode = "P00004", Price = 150.55M },
                new Product { Id = 5, Name = "Trombone", ProductCode = "P00005", Price = 150.55M },
                new Product { Id = 6, Name = "Violin", ProductCode = "P00006", Price = 150.55M },
                new Product { Id = 7, Name = "Guitar", ProductCode = "P00007", Price = 150.55M },
                new Product { Id = 8, Name = "Bagpipes", ProductCode = "P00008", Price = 150.55M },
                new Product { Id = 9, Name = "Ukulele", ProductCode = "P00009", Price = 150.55M },
                new Product { Id = 10, Name = "Saxophone", ProductCode = "P00010", Price = 150.55M }
              );
            #endregion

            #region Store Seed
            // Seeds UserType data
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "Florida" },
                new Store { Id = 2, Name = "New York" },
                new Store { Id = 3, Name = "Texas" },
                new Store { Id = 4, Name = "Washington" },
                new Store { Id = 5, Name = "California" }

            );
            #endregion

            #region Inventory Seed
            modelBuilder.Entity<InventoryItem>().HasData(

                new InventoryItem { Id = 1, StoreId = 1, ProductId = 1, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 2, StoreId = 1, ProductId = 2, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 3, StoreId = 1, ProductId = 3, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 4, StoreId = 1, ProductId = 4, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 5, StoreId = 1, ProductId = 5, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 6, StoreId = 1, ProductId = 6, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 7, StoreId = 1, ProductId = 7, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 8, StoreId = 1, ProductId = 8, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 9, StoreId = 1, ProductId = 9, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 10, StoreId = 1, ProductId = 10, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },

                new InventoryItem { Id = 11, StoreId = 2, ProductId = 1, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 12, StoreId = 2, ProductId = 2, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 13, StoreId = 2, ProductId = 3, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 14, StoreId = 2, ProductId = 4, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 15, StoreId = 2, ProductId = 5, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 16, StoreId = 2, ProductId = 6, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 17, StoreId = 2, ProductId = 7, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 18, StoreId = 2, ProductId = 8, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 19, StoreId = 2, ProductId = 9, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 20, StoreId = 2, ProductId = 10, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },


                new InventoryItem { Id = 21, StoreId = 3, ProductId = 1, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 22, StoreId = 3, ProductId = 2, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 23, StoreId = 3, ProductId = 3, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 24, StoreId = 3, ProductId = 4, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 25, StoreId = 3, ProductId = 5, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 26, StoreId = 3, ProductId = 6, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 27, StoreId = 3, ProductId = 7, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 28, StoreId = 3, ProductId = 8, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 29, StoreId = 3, ProductId = 9, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 30, StoreId = 3, ProductId = 10, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },

                new InventoryItem { Id = 31, StoreId = 4, ProductId = 1, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 32, StoreId = 4, ProductId = 2, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 33, StoreId = 4, ProductId = 3, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 34, StoreId = 4, ProductId = 4, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 35, StoreId = 4, ProductId = 5, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 36, StoreId = 4, ProductId = 6, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 37, StoreId = 4, ProductId = 7, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 38, StoreId = 4, ProductId = 8, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 39, StoreId = 4, ProductId = 9, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 40, StoreId = 4, ProductId = 10, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },

                new InventoryItem { Id = 41, StoreId = 5, ProductId = 1, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 42, StoreId = 5, ProductId = 2, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 43, StoreId = 5, ProductId = 3, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 44, StoreId = 5, ProductId = 4, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 45, StoreId = 5, ProductId = 5, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 46, StoreId = 5, ProductId = 6, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 47, StoreId = 5, ProductId = 7, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 48, StoreId = 5, ProductId = 8, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 49, StoreId = 5, ProductId = 9, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() },
                new InventoryItem { Id = 50, StoreId = 5, ProductId = 10, Quantity = 20, LoggedUserId = 1, ChangedDate = DateTime.Now.ToLocalTime() }

                );
            #endregion

            #region Order Seed

            modelBuilder.Entity<Order>().HasData(
               // Same Customer same store
               // First Same customer same store *
               new Order { Id = 1, CustomerId = 1, StoreId = 1, OrderDate = DateTimeOffset.Now },
               new Order { Id = 2, CustomerId = 1, StoreId = 1, OrderDate = DateTimeOffset.Now },

               // Different Customer same Store
               new Order { Id = 3, CustomerId = 2, StoreId = 2, OrderDate = DateTimeOffset.Now },
               new Order { Id = 4, CustomerId = 3, StoreId = 2, OrderDate = DateTimeOffset.Now },

               // Different Customer different Stores
               new Order { Id = 5, CustomerId = 4, StoreId = 4, OrderDate = DateTimeOffset.Now },
               new Order { Id = 6, CustomerId = 5, StoreId = 5, OrderDate = DateTimeOffset.Now }
            );


            #endregion

            #region OrderLineItem Seed

            modelBuilder.Entity<OrderLineItem>().HasData(

                // Same Customer same store ( CustomerId 1 ) ( subtract quantity in inventory for each.) 
                // ( add prices of same orderId for result only, only when queried. ) 

                new OrderLineItem { Id = 1, OrderId = 1, InventoryItemId = 1, Quantity = 2, Price = 150.55M, },
                new OrderLineItem { Id = 2, OrderId = 1, InventoryItemId = 2, Quantity = 4, Price = 150.55M },
                new OrderLineItem { Id = 3, OrderId = 1, InventoryItemId = 3, Quantity = 5, Price = 150.55M },
                new OrderLineItem { Id = 4, OrderId = 1, InventoryItemId = 4, Quantity = 7, Price = 150.55M },


                new OrderLineItem { Id = 5, OrderId = 2, InventoryItemId = 1, Quantity = 3, Price = 150.55M },

                new OrderLineItem { Id = 6, OrderId = 3, InventoryItemId = 1, Quantity = 2, Price = 150.55M }
            );
            #endregion
        }
    }
}
