using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MvcMovie.Domain;
using System.ComponentModel;

namespace MvcMovie.DataAccess
{
    public class MvcAppDbContext:DbContext
    {
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
