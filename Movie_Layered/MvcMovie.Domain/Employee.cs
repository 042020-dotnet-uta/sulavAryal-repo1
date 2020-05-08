using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcMovie.DataAccess
{
	public class Employee
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int DepartmentId { get; set; }
		public Department Department { get; set; }
	}
}
