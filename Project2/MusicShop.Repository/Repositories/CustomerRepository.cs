﻿using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly MSDbContext _context;

        public CustomerRepository(MSDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Find Customer by Id 
        /// </summary>
        /// <param name="id">int</param>
        /// <returns></returns>
        public async Task<Customer> FindCustomerById(int? id)
        {
            try
            {
                return await _context.Customers
                    .Include(c => c.CustomerAddress)
                    .AsNoTracking()
                    .Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Validates the user if username and password match.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task<bool> ValidateCustomer(string username, string password)
        {
            try
            {
                var result = await _context.Customers
              .AsNoTracking()
              .Where(c => c.Email == username && c.Password == password).FirstOrDefaultAsync();
                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }
    }
}
