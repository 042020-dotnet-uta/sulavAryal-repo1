using MvcMovie.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MvcMovie.DataAccess
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Employee> Employee { get; set; }

		public Department()
		{
			this.Employee = new List<Employee>();
		}
	}
}
