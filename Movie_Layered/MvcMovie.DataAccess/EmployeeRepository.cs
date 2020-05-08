using Microsoft.EntityFrameworkCore;
using MvcMovie.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcMovie.DataAccess
{
	
	public static class EmployeeRepository
 	{
		
		public static List<Employee> GetAllEmployees(MvcAppDbContext dbContext)
		{
			var result =  dbContext.Employees.Include(x=>x.Department).Select(x => x).ToList();
			return result;
		}

	}

	
}
