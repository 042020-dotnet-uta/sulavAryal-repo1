using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using MusicShop.UI;
using MusicShop.UI.Controllers;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicShop.Tests
{
    public class RepositoryTests
    {

        //[Fact]
        //public async void GetsCustomerById()
        //{
        //    //Arrange
        //    var options = InMemoryDb("GetsCustomer");
        //    string firstName = "Ram";
        //    string lastName = "Kumar";
        //    string email = "ram@test.com";
        //    int id = 1;

        //    //Act
        //    using (var context = new MSDbContext(options))
        //    {
        //        var customer = new Customer
        //        {
        //            Id = id,
        //            FirstName = firstName,
        //            LastName = lastName,
        //            Email = email
        //        };
        //        context.Add(customer);
        //        context.SaveChanges();
        //    }
        //    //Assert
        //    using (var context = new MSDbContext(options))
        //    {
        //        Ilogger fakeLogger = new Logger();
        //        var customerRepo = new CustomerRepository(context);
        //        var customerInfo = await customerRepo.FindCustomerById(id);

        //        Assert.Equal(firstName, customerInfo.FirstName);
        //        Assert.Equal(lastName, customerInfo.LastName);
        //        Assert.Equal(email, customerInfo.Email);
        //    }
        //}

        //[Fact]
        //public async Task AddsCustomerToDbAsync()
        //{
        //    //Arrange
        //    var options = InMemoryDb("AddsCustomer");

        //    string fName = "Row", lName = "K", email = "rk@gmail.com";
        //    var cutomerInfo1 = new Customer
        //    {
        //        Email = email,
        //        FirstName = fName,
        //        LastName = lName
        //    };

        //    //Act
        //    using (var context = new MSDbContext(options))
        //    {
            
        //        var customerRepo = new CustomerRepository(context);
        //        context.Add(cutomerInfo1);
        //        await context.SaveChangesAsync();


        //        Assert.Equal(11, cutomerInfo1.Id);
        //        Assert.Equal(fName, cutomerInfo1.FirstName);
        //        Assert.Equal(lName, cutomerInfo1.LastName);
        //        Assert.Equal(email, cutomerInfo1.Email);
        //    }

        //}


        //[Fact]
        //public async void GetCustomerByEmail()
        //{
        //    //Arrange
        //    var options = InMemoryDb("GetsCustomerByEmail");
        //    string fName = "Person";
        //    string lName = "withLastName";
        //    string email = "person@gmail.com";
        //    int id = 3;

        //    //Act
        //    using (var context = new MSDbContext(options))
        //    {
        //        var customer = new Customer
        //        {
        //            Id = id,
        //            FirstName = fName,
        //            LastName = lName,
        //            Email = email
        //        };
        //        context.Add(customer);
        //        context.SaveChanges();
        //    }
        //    //Assert
        //    using (var context = new MSDbContext(options))
        //    {
        //        var customerRepo = new CustomerRepository(context);
        //        var list = await customerRepo.FindByAsync(c => c.Email == email);
        //        var customerInfo = list.FirstOrDefault();

        //        Assert.Equal(id, customerInfo.Id);
        //        Assert.Equal(fName, customerInfo.FirstName);
        //        Assert.Equal(lName, customerInfo.LastName);
        //        Assert.Equal(email, customerInfo.Email);
        //    }
        //}

        [Fact]
        public async Task GetsAllLocations()
        {
            //Arrange
            var options = InMemoryDb("GetsAllLocations");

            //Act
            using (var context = new MSDbContext(options))
            {
                var store = new Store
                {
                    Name = "Florida"
                };
                context.Add(store);
                store = new Store
                {
                    Name = "Texas"
                };
                context.Add(store);
                store = new Store
                {
                    Name = "Washinton"
                };
                context.Add(store);
                await context.SaveChangesAsync();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                var stores = context.Stores.Select(x => x);
                //var stores = await storeRepo.();

                Assert.Equal(4, stores.Count());
            }
        }


        [Fact]
        public async void GetsAllProducts()
        {
            //Arrange
            var options = InMemoryDb("GetsAllProducts");

            //Act
            using (var context = new MSDbContext(options))
            {
                var product = new Product
                {
                    Id = 6,
                    Price = 150.55M,
                };
                context.Add(product);
                product = new Product
                {
                    Id = 7,
                    Price = 150.55M,
                };
                context.Add(product);
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                var productRepo = new ProductRepository(context);
                var products = await productRepo.GetAllAsync();

                Assert.Equal(6, products.Count());
            }
        }



        [Fact]
        public async void GetsAllOrdersForLocation()
        {
            //Arrange
            var options = InMemoryDb("GetsLocationOrders");

            //Act
            using (var context = new MSDbContext(options))
            {
                var customer = new Customer
                {
                    Id = 10,
                    FirstName = "Conan",
                    LastName = "perse",
                    Email = "konan@gmail.com"
                };
                context.Add(customer);
                context.SaveChanges();

                var product = new Product
                {
                    Id = 11,
                    Price = 150.55M,
                };
                context.Add(product);
                product = new Product
                {
                    Id = 12,
                    Price = 150.55M,
                };
                context.Add(product);
                context.SaveChanges();

                var store = new Store
                {
                    Id = 4,
                    Name = "Plorida"
                };
                context.Add(store);
                context.SaveChanges();

                var order = new Order
                {
                    Id = 13,
                    OrderDate = DateTime.Now,
                    StoreId = 1,
                };
                context.Add(order);
                order = new Order
                {
                    Id = 3,
                    OrderDate = DateTime.Now,
                    StoreId = 1,
                };
                context.Add(order);
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                IHttpContextAccessor contextAccessor = new HttpContextAccessor();
                var shoppingCartRepo = new ShoppingCart(context);
                var orderRepo = new OrderService(context, shoppingCartRepo, contextAccessor);
                var orders = await orderRepo.FindAsync(o => o.StoreId == 1 && o.Id == 2);

                Assert.Equal(0, orders.Select(i => i.Id).FirstOrDefault());
            }
        }

        [Fact]
        public async void GetsAllOrdersForCustomer()
        {
            //Arrange
            var options = InMemoryDb("GetsCustomersOrders");

            //Act
            using (var context = new MSDbContext(options))
            {
                var customer = new Customer
                {
                    Id = 4,
                    FirstName = "Conan",
                    LastName = "perse",
                    Email = "konan@gmail.com"
                };
                context.Add(customer);
                context.SaveChanges();
                CreateTwoproducts(context);

                var store = new Store
                {
                    Id = 7,
                    Name = "SomeLocation"
                };
                context.Add(store);
                context.SaveChanges();

                var order = new Order
                {
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    StoreId = 3,
                };
                context.Add(order);
                order = new Order
                {
                    CustomerId = 6,
                    OrderDate = DateTime.Now,
                    StoreId = 5,
                };
                context.Add(order);
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                IHttpContextAccessor contextAccessor = new HttpContextAccessor();
                var shoppingCartRepo = new ShoppingCart(context);
                var orderRepo = new OrderService(context, shoppingCartRepo, contextAccessor);
                var orders = await orderRepo.FindAsync(o => o.CustomerId == 1);

                Assert.Empty(orders);
            }
        }

        [Fact]
        public void AddsStoreToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "AddsStoreToDbTest")
                .Options;

            //Act
            using (var db = new MSDbContext(options))
            {
                Store location = new Store
                {
                    Name = "Spokane"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(1, context.Stores.Count());

                var store2 = context.Stores.Where(s => s.Id == 1).FirstOrDefault();
                Assert.Equal(1, store2.Id);
                Assert.Equal("Spokane", store2.Name);
            }
        }

        private void CreateOneCustomer(MSDbContext context)
        {
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Conan",
                LastName = "perse",
                Email = "konan@gmail.com"
            };
            context.Add(customer);
            context.SaveChanges();
        }

        private void CreateTwoproducts(MSDbContext context)
        {
            var product = new Product
            {
                Id = 1,
                Price = 150.55M,
            };
            context.Add(product);
            product = new Product
            {
                Id = 2,
                Price = 150.55M,
            };
            context.Add(product);
            context.SaveChanges();
        }

        [Fact]
        public void AddsProductToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "AddsProductToDbTest")
                .Options;

            //Act
            using (var db = new MSDbContext(options))
            {
                Product sitar = new Product
                {
                    Id = 17,
                    Name = "sitar",
                    Price = 100M
                };

                db.Add(sitar);
                db.SaveChanges();
            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(1, context.Products.Count());

                var product1 = context.Products.Where(p => p.Id == 17).FirstOrDefault();
                Assert.Equal(17, product1.Id);
                Assert.Equal("sitar", product1.Name);
            }
        }

        [Fact]
        public async Task DeletesProductFromDb()
        {

            //Arrange - create an object to configure your inmemory DB.
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletesProductFromDb")
                .Options;


            //Act - send in the configure object to the DbContext constructor to be used in configuring the DbContext
            using (var db = new MSDbContext(options))
            {
                Product product = new Product
                {
                    Id = 26,
                    Name = "drum",
                };
                db.Add(product);
                await db.SaveChangesAsync();
                db.Remove(product);
                await db.SaveChangesAsync();

            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(0, context.Products.Count());

            }
        }


        [Fact]
        public async Task DeletesStoreFromDb()
        {

            //Arrange - create an object to configure your inmemory DB.
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletesStoreFromDb")
                .Options;


            //Act - send in the configure object to the DbContext constructor to be used in configuring the DbContext
            using (var db = new MSDbContext(options))
            {
                Store store = new Store
                {
                    Id = 42,
                    Name = "Mars",
                };
                db.Add(store);
                await db.SaveChangesAsync();
                db.Remove(store);
                await db.SaveChangesAsync();

            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(0, context.Stores.Count());

            }
        }

        [Fact]
        public async Task DeletesOrderFromDb()
        {

            //Arrange - create an object to configure your inmemory DB.
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletesOrderFromDb")
                .Options;


            //Act - send in the configure object to the DbContext constructor to be used in configuring the DbContext
            using (var db = new MSDbContext(options))
            {
                Order order = new Order
                {
                    Id = 43,
                    StoreId = 4
                };
                db.Add(order);
                await db.SaveChangesAsync();
                db.Remove(order);
                await db.SaveChangesAsync();

            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(0, context.Orders.Count());

            }
        }

        [Fact]
        public void GetsOrderLineItem()
        {
            var options = InMemoryDb("GetsOrderLineItems");

            //Act
            using (var context = new MSDbContext(options))
            {
                var orderLineItem = new OrderLineItem
                {
                    Id = 45,
                    OrderId = 5,
                    Price = 25M,
                    Quantity = 4
                };
                context.Add(orderLineItem);
                context.SaveChanges();

                // Assert
                var result = context.OrderLineItems.Where(o => o.Id == orderLineItem.Id).AsNoTracking().FirstOrDefault();

                Assert.Equal(orderLineItem.Quantity, result.Quantity);
            }

        }


        [Fact]
        public void CustomerSearchByUserByFirstName()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerByName")
                .Options;
            Customer cust = new Customer
            {
                FirstName = "Test User",
                LastName = "Test Last"

            };

            string firstName = "Test";

            //Act
            using (var context = new MSDbContext(options))
            {

                context.Add(cust);
                context.SaveChanges();

                var result = context.Customers
                    .Where(c => c.FirstName.Contains(firstName))
                    .AsNoTracking().FirstOrDefault();

                //Assert
                Assert.Equal(cust.FirstName, result.FirstName);
            }


        }

        [Fact]
        public void CustomerSearchByUserByLastName()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerByName")
                .Options;
            Customer cust = new Customer
            {
                FirstName = "Test User",
                LastName = "Test Last"

            };

            string lastName = "Test";

            //Act
            using (var context = new MSDbContext(options))
            {

                context.Add(cust);
                context.SaveChanges();

                var result = context.Customers
                    .Where(c => c.LastName.Contains(lastName))
                    .AsNoTracking().FirstOrDefault();

                //Assert
                Assert.Equal(cust.LastName, result.LastName);
            }


        }

        [Fact]
        public void ShoppingCartHasItems()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerByName")
                .Options;
            ShoppingCartItem cartItem = new ShoppingCartItem
            {
                ShoppingCartId = "1",
                customerEmail = "test@test.com",
                Quantity = 2,
                StoreId = "5"

            };


            //Act
            using (var context = new MSDbContext(options))
            {

                context.Add(cartItem);
                context.SaveChanges();

                var result = context.ShoppingCartItems
                    .Where(c => c.customerEmail.Contains(cartItem.customerEmail))
                    .AsNoTracking().FirstOrDefault();

                //Assert
                Assert.Equal(cartItem.customerEmail, result.customerEmail);
            }


        }

        private DbContextOptions<MSDbContext> InMemoryDb(string name)
        {
            return new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "someDb")
                .Options;
        }
    }
}
