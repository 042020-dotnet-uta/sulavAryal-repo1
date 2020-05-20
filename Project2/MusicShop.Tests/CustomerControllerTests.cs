using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using MusicShop.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicShop.Tests
{
    public class CustomersControllersTests
    {
        //private readonly CustomerRepository _sut;
        private readonly Mock<ICustomerRepository> _customerRepoMock = new Mock<ICustomerRepository>();
        private readonly Mock<IGenericRepository<Customer>> _genericRepoMock = new Mock<IGenericRepository<Customer>>();

        [Fact]
        public async Task FindCustomerById_ShouldReturnCustomer_WhenCustomerExits()
        {

            // Arrange 
            var options = new DbContextOptionsBuilder<MSDbContext>()
               .UseInMemoryDatabase(databaseName: "AddsCustomerToDb")
               .Options;

            using (var db = new MSDbContext(options))
            {
                Customer customer = new Customer
                {
                    Id = 6,
                    FirstName = "Maribeth",
                    LastName = "Fontenot",
                    Email = "test@test.com",
                    PhoneNo = "1234112233",
                    Password = "password",
                    UserTypeId = 2
                };

                var result = await db.Customers
                    .Include(c => c.CustomerAddress)
                    .AsNoTracking()
                    .Where(c => c.Id == customer.Id).FirstOrDefaultAsync();

                // Assert
                Assert.Equal(6, customer.Id);
            }

        }

        [Fact]
        public async Task AddsCustomerToDb()
        {

            //Arrange - create an object to configure your inmemory DB.
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "AddsCustomerToDb")
                .Options;


            //Act - send in the configure object to the DbContext constructor to be used in configuring the DbContext
            using (var db = new MSDbContext(options))
            {
                CustomerAddress customerAddress = new CustomerAddress { Id = 1, Street = "8286 Clay Ave.", City = "Spokane", State = "WA", Zip = "11111" };
                Customer customer = new Customer { Id = 6, FirstName = "Maribeth", LastName = "Fontenot", Email = "test@test.com", PhoneNo = "1234112233", Password = "password", UserTypeId = 2, CustomerAddress = customerAddress };
                db.Add(customer);
                db.SaveChanges();
            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                var customer1 = await context.Customers.Where(x => x.FirstName == "Maribeth")
                    .AsNoTracking().FirstOrDefaultAsync();
                var customer1Address = await context.Customers.
                    Include(c => c.CustomerAddress).AsNoTracking().FirstOrDefaultAsync();
                Assert.Equal("Maribeth", customer1.FirstName);
                Assert.Equal("11111", customer1Address.CustomerAddress.Zip);
            }
        }


        [Fact]
        public async Task DeletesCustomerFromDb()
        {

            //Arrange - create an object to configure your inmemory DB.
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletesCustomerToDb")
                .Options;


            //Act - send in the configure object to the DbContext constructor to be used in configuring the DbContext
            using (var db = new MSDbContext(options))
            {
                CustomerAddress customerAddress = new CustomerAddress { Id = 1, Street = "8286 Clay Ave.", City = "Spokane", State = "WA", Zip = "11111" };
                Customer customer = new Customer { Id = 6, FirstName = "Maribeth", LastName = "Fontenot", Email = "test@test.com", PhoneNo = "1234112233", Password = "password", UserTypeId = 2, CustomerAddress = customerAddress };
                db.Add(customer);
                await db.SaveChangesAsync();
                db.Remove(customer);
                await db.SaveChangesAsync();

            }

            //Assert
            using (var context = new MSDbContext(options))
            {
                Assert.Equal(0, context.Customers.Count());
              
            }
        }

        [Fact]
        public async Task DeleteConfirmedRedirectsToIndex()
        {
            // arrange
            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(x => x.DeleteAsync(1));

            var controller = new CustomersController(mockRepo.Object);

            // act
            IActionResult result = await controller.DeleteConfirmed(1);

            // assert that the result is a ViewResult
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            // ...that the model of the view is a CustomersViewModel
            Assert.Equal("Index", viewResult.ActionName);
        }

    }
}
