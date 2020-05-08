using Microsoft.EntityFrameworkCore;
using MvcMovie.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcMovie.DataAccess
{
	
	public static class DepartmentRepository
 	{
		
		public static List<Department> GetAllDepartments(MvcAppDbContext dbContext)
		{
			var result =  dbContext.Departments.Include(x=>x.Employee).Select(x => x).ToList();
			return result;
		}

		public static int GetAllDepartmentIdByName(MvcAppDbContext dbContext,string name)
		{
			var result = dbContext.Departments.Include(x => x.Employee).Where(x=>x.Name == name).Select(x => x.Id).FirstOrDefault();
			return result;
		}

	}

	
}
