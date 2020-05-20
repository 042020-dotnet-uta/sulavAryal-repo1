using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using MusicShop.UI;
using MusicShop.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MusicShop.Tests
{
    public class RepositoryTests
    {
       
        [Fact]
        public async void GetsCustomerById()
        {
            //Arrange
            var options = InMemoryDb("GetsCustomer");
            string firstName = "Ram";
            string lastName = "Kumar";
            string email = "ram@test.com";
            int id = 1;

            //Act
            using (var context = new MSDbContext(options))
            {
                var customer = new Customer
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
                context.Add(customer);
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                var customerRepo = new CustomerRepository(context);
                var customerInfo = await customerRepo.FindCustomerById(id);

                Assert.Equal(firstName, customerInfo.FirstName);
                Assert.Equal(lastName, customerInfo.LastName);
                Assert.Equal(email, customerInfo.Email);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task AddsCustomerToDbAsync()
        {
            //Arrange
            var options = InMemoryDb("AddsCustomer");

            string fName = "Row", lName = "K", email = "rk@gmail.com";
            var cutomerInfo1 = new Customer
            {
                Email = email,
                FirstName = fName,
                LastName = lName
            };

            //Act
            using (var context = new MSDbContext(options))
            {
                var customerRepo = new CustomerRepository(context);
                context.Add(cutomerInfo1);
                context.SaveChanges();


                Assert.Equal(11, cutomerInfo1.Id);
                Assert.Equal(fName, cutomerInfo1.FirstName);
                Assert.Equal(lName, cutomerInfo1.LastName);
                Assert.Equal(email, cutomerInfo1.Email);
            }
          
        }


        [Fact]
        public async void GetCustomerByEmail()
        {
            //Arrange
            var options = InMemoryDb("GetsCustomerByEmail");
            string fName = "Person";
            string lName = "withLastName"; 
            string email = "person@gmail.com";
            int id = 3;

            //Act
            using (var context = new MSDbContext(options))
            {
                var customer = new Customer
                {
                    Id = id,
                    FirstName = fName,
                    LastName = lName,
                    Email = email
                };
                context.Add(customer);
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                var customerRepo = new CustomerRepository(context);
                var list = await customerRepo.FindByAsync(c => c.Email == email);
                var customerInfo = list.FirstOrDefault();

                Assert.Equal(id, customerInfo.Id);
                Assert.Equal(fName, customerInfo.FirstName);
                Assert.Equal(lName, customerInfo.LastName);
                Assert.Equal(email, customerInfo.Email);
            }
        }

        [Fact]
        public async  void GetsAllLocations()
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
                context.SaveChanges();
            }
            //Assert
            using (var context = new MSDbContext(options))
            {
                var stores = context.Stores.Select(x=>x);
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

                Assert.Equal(0, orders.Select(i=>i.Id).FirstOrDefault());
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

                Assert.Equal(0, orders.Count());
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

        private DbContextOptions<MSDbContext> InMemoryDb(string name)
        {
            return new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "someDb")
                .Options;
        }
    }
}
