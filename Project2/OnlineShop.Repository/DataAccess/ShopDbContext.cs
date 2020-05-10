﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Repository.DataAccess
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}